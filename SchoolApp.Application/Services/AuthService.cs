using System.IdentityModel.Tokens.Jwt;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Auth;
using SchoolApp.Application.Helpers;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;
using SchoolApp.Domain.Results.Raw;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginDTO> _loginValidator;
    private readonly IGenericRepository _genericRepository;
    public AuthService(IValidator<LoginDTO> loginValidator, IGenericRepository genericRepository, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _genericRepository = genericRepository;
        _loginValidator = loginValidator;
    }
    public async Task<IServiceResultWithData<TokenResponseDTO>> GenerateAccessTokenWithRefreshTokenAsync(RefreshTokenRequestDTO dto)
    {
        var refreshToken = await _genericRepository.GetAll<RefreshToken>()
                            .FirstOrDefaultAsync(rt => rt.Token == dto.Token);

        if (refreshToken is null)
            return new ErrorResultWithData<TokenResponseDTO>("Invalid token.");

        var accesToken = await _genericRepository.GetAll<AccessToken>()
                            .Where(a => a.RefreshToken == refreshToken.Token)
                            .FirstOrDefaultAsync();


        if (refreshToken.ExpiresAt <= DateTime.UtcNow)
        {
            _genericRepository.Delete(refreshToken);
            await _genericRepository.SaveChangesAsync();

            return new ErrorResultWithData<TokenResponseDTO>("Token expired.");
        }

        if (refreshToken.IsUsed)
            return new ErrorResultWithData<TokenResponseDTO>("Refresh token has already been used.");


        var user = await _genericRepository.GetAll<User>()
                            .Include(u => u.Role)
                            .FirstOrDefaultAsync(u => u.Id == refreshToken.UserId);

        if (user is null)
            return new ErrorResultWithData<TokenResponseDTO>("User no longer exist.");

        var accessTokenResult = _tokenService.GenerateJwtAccessToken(user);

        if (!accessTokenResult.Success)
            return new ErrorResultWithData<TokenResponseDTO>("Access token error.");

        var accessToken = new AccessToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(accessTokenResult.Data),
            ExpiresAt = accessTokenResult.Data.ValidTo,
            RefreshToken = refreshToken.Token,
            UserId = user.Id
        };

        refreshToken.IsUsed = true;

        await _genericRepository.Add(accessToken);
        await _genericRepository.SaveChangesAsync();

        var result = new TokenResponseDTO
        {
            AccessToken = accessToken.Token,
            AccessTokenExpiresAt = accessToken.ExpiresAt,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiresAt = refreshToken.ExpiresAt
        };

        return new SuccessResultWithData<TokenResponseDTO>("Access token updated.", result);
    }
    public async Task<IServiceResultWithData<User>> Me(int userId)
    {
        var user = await _genericRepository.GetAll<User>().Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            return new ErrorResultWithData<User>($"Invalid token.");

        return new SuccessResultWithData<User>("Me: ", user);
    }

    public async Task<IServiceResult> LogOutAsync(string accessTokenString)
    {
        var accessToken = await _genericRepository.GetAll<AccessToken>()
                                .FirstOrDefaultAsync(at => at.Token == accessTokenString);

        if (accessToken is null)
            return new ErrorResult("Invalid token.");

        var refreshToken = await _genericRepository.GetAll<RefreshToken>()
                                .FirstOrDefaultAsync(rt => rt.Token == accessToken.RefreshToken);

        if (refreshToken is null)
            return new ErrorResult("Refresh token not found.");

        accessToken.IsRevoked = true;
        refreshToken.IsRevoked = true;

        await _genericRepository.UpdateAsync(accessToken);
        await _genericRepository.UpdateAsync(refreshToken);
        await _genericRepository.SaveChangesAsync();

        return new SuccessResult("Logged out successfully.");
    }
    public async Task<bool> IsTokenRevokedAsync(string token)
    {
        var accessToken = await _genericRepository.GetAll<AccessToken>()
            .FirstOrDefaultAsync(at => at.Token == token);

        return accessToken != null && accessToken.IsRevoked;
    }

    public async Task<IServiceResultWithData<TokenResponseDTO>> LoginAsync(LoginDTO dto)
    {
        try
        {
            var validationResult = await _loginValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return new ErrorResultWithData<TokenResponseDTO>(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var user = await _genericRepository.GetAll<User>()
                        .Include(u => u.Role)
                        .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user is null || user.IsDeleted)
                return new ErrorResultWithData<TokenResponseDTO>("Auth failed.");

            if (!HashingHelper.VerifyPasswordHash(dto.Password, user.Hash, user.Salt))
                return new ErrorResultWithData<TokenResponseDTO>("Auth failed.");


            var jwtTokenResult = _tokenService.GenerateJwtAccessToken(user);

            if (!jwtTokenResult.Success)
                return new ErrorResultWithData<TokenResponseDTO>("Error occured while creating a access token.");

            var refreshTokenResult = _tokenService.GenerateRefreshToken(user);

            if (!refreshTokenResult.Success)
                return new ErrorResultWithData<TokenResponseDTO>("Error occured while creating a refresh token.");

            var accessJwtToken = jwtTokenResult.Data;

            var refreshToken = refreshTokenResult.Data;
            
            var accessToken = new AccessToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(accessJwtToken),
                ExpiresAt = accessJwtToken.ValidTo,
                UserId = user.Id,
                RefreshToken = refreshToken.Token
            };

            var response = new TokenResponseDTO
            {
                AccessToken = accessToken.Token,
                AccessTokenExpiresAt = accessToken.ExpiresAt,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresAt = refreshToken.ExpiresAt
            };

            await _genericRepository.Add(refreshToken);
            await _genericRepository.Add(accessToken);
            await _genericRepository.SaveChangesAsync();

            return new SuccessResultWithData<TokenResponseDTO>("Login successful.", response);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<TokenResponseDTO>(ex.Message);
        }
    }
}