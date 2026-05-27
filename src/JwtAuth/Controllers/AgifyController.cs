using Microsoft.AspNetCore.Mvc;
using JwtAuth;

namespace JwtAuth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgifyController : ControllerBase
{
    private static readonly HttpClient Client = new();

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

    [HttpGet("predict")]
    public async Task<ActionResult<AgifyAgeDto>> PredictAge([FromQuery] string name)
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
