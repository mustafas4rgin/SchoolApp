using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Auth;
using SchoolApp.Application.Services;
using SchoolApp.Domain.Results;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
        var token = authHeader.StartsWith("Bearer ") ? authHeader.Substring("Bearer ".Length) : authHeader;

        var result = await _authService.LogOutAsync(token);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = CurrentUserId;

        if (userId == null)
            return Unauthorized("Invalid token.");

        var result = await _authService.Me(userId.Value);

        if (!result.Success)
            return BadRequest(result.Message);

        var user = result.Data;

        return Ok(new
        {
            FirstName = user.FirstName + " " + user.LastName,
            Role = user.Role.Name,
        });
    }

    [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO dto)
        {
            var tokenResult = await _authService.GenerateAccessTokenWithRefreshTokenAsync(dto);

            if (!tokenResult.Success)
                return BadRequest(tokenResult.Message);

            return Ok(tokenResult.Data);
        }
}
