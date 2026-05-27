using Microsoft.AspNetCore.Mvc;
using JwtAuth.Services;

namespace JwtAuth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    /// <summary>
    /// Generate JWT token with custom username and expiry duration
    /// </summary>
    /// <param name="request">Request containing username and expiration minutes</param>
    /// <returns>JWT token with custom expiry</returns>
    [HttpPost("generate-token")]
    public ActionResult<TokenResponse> GenerateToken([FromBody] GenerateTokenRequest request)
    {
        if (string.IsNullOrEmpty(request.Username))
        {
            return BadRequest(new { error = "Username is required" });
        }

        if (request.ExpirationMinutes <= 0)
        {
            return BadRequest(new { error = "Expiration minutes must be greater than 0" });
        }

        var token = _jwtService.GenerateToken(request.Username, request.ExpirationMinutes);
        var expiresAt = DateTime.UtcNow.AddMinutes(request.ExpirationMinutes);

        return Ok(new TokenResponse
        {
            Token = token,
            Username = request.Username,
            ExpiresAt = expiresAt
        });
    }
}
