using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ClaimsAuth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgifyController : ControllerBase
{
    private static readonly HttpClient Client = new();

    [HttpGet("age/{name}")]
    public async Task<ActionResult<AgifyAgeDto>> GetAge(string name)
    {
        var authHeader = Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            return Unauthorized(new { error = "Missing or invalid Authorization header" });

        var token = authHeader.Substring("Bearer ".Length).Trim();

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var usernameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "username");
            if (usernameClaim == null) { return BadRequest(new { error = "Token missing username claim" }); }
            if (jwtToken.ValidTo < DateTime.UtcNow) { return Unauthorized(new { error = "Token has expired" }); }

            var response = await Client.GetAsync($"https://api.agify.io/?name={name}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<AgifyAgeDto>(json);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}