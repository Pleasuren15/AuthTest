using Microsoft.AspNetCore.Mvc;
using JwtAuth.Services;

namespace JwtAuth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(IJwtService jwtService, JwtSettings jwtSettings)
    {
        _jwtService = jwtService;
        _jwtSettings = jwtSettings;
    }

    /// <summary>
    /// Generate JWT token with username and custom expiry
    /// </summary>
    /// <param name="request">Login request with username and password</param>
    /// <returns>JWT token with expiry information</returns>
    [HttpPost("login")]
    public ActionResult<TokenResponse> Login([FromBody] LoginRequest request)
    {
        // TODO: Validate credentials against your database/identity provider
        // This is a simple example - replace with real authentication
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { error = "Username and password are required" });
        }

        var token = _jwtService.GenerateToken(request.Username, _jwtSettings.ExpirationMinutes);
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

        return Ok(new TokenResponse
        {
            Token = token,
            Username = request.Username,
            ExpiresAt = expiresAt
        });
    }

    /// <summary>
    /// Generate JWT token with custom expiry duration
    /// </summary>
    /// <param name="username">Username for the token</param>
    /// <param name="expirationMinutes">Custom expiration time in minutes</param>
    /// <returns>JWT token with custom expiry</returns>
    [HttpPost("generate-token")]
    public ActionResult<TokenResponse> GenerateToken(
        [FromQuery] string username,
        [FromQuery] int expirationMinutes = 60)
    {
        if (string.IsNullOrEmpty(username))
        {
            return BadRequest(new { error = "Username is required" });
        }

        if (expirationMinutes <= 0)
        {
            return BadRequest(new { error = "Expiration minutes must be greater than 0" });
        }

        var token = _jwtService.GenerateToken(username, expirationMinutes);
        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

        return Ok(new TokenResponse
        {
            Token = token,
            Username = username,
            ExpiresAt = expiresAt
        });
    }
}
