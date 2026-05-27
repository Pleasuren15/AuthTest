using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JwtAuth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgifyController : ControllerBase
{
    private static readonly HttpClient Client = new();

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { message = "Agify service is healthy" });
    }

    [Authorize]
    [HttpGet("age/{name}")]
    public async Task<ActionResult<AgifyAgeDto>> GetAge(string name)
    {
        try
        {
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

