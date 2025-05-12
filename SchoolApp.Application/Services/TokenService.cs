using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolApp.Application.Concrete;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    public TokenService(IConfiguration config)
    {
        _config = config;
    }
    public IServiceResultWithData<JwtSecurityToken> GenerateJwtAccessToken(User user)
    {
        try
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds);

            return new SuccessResultWithData<JwtSecurityToken>("Token created.",token);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<JwtSecurityToken>(ex.Message);
        }
    }
    public IServiceResultWithData<RefreshToken> GenerateRefreshToken(User user)
    {
        try
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var refreshToken = new RefreshToken
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };

            return new SuccessResultWithData<RefreshToken>("Refresh token created.", refreshToken);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<RefreshToken>(ex.Message);
        }
    }
    
}