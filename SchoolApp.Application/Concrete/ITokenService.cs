using System.IdentityModel.Tokens.Jwt;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Concrete;

public interface ITokenService
{
    IServiceResultWithData<JwtSecurityToken> GenerateJwtAccessToken(User user);
    IServiceResultWithData<RefreshToken> GenerateRefreshToken(User user);

}