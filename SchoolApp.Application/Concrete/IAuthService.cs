using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Auth;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace SchoolApp.Application.Concrete;

public interface IAuthService
{
    Task<IServiceResultWithData<TokenResponseDTO>> GenerateAccessTokenWithRefreshTokenAsync(RefreshTokenRequestDTO dto);
    Task<IServiceResultWithData<TokenResponseDTO>> LoginAsync(LoginDTO dto);
    Task<IServiceResult> LogOutAsync(string accessTokenString);
    Task<IServiceResultWithData<User>> Me(int userId);
    Task<bool> IsTokenRevokedAsync(string token);
}