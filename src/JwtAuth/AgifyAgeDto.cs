using System.Text.Json.Serialization;

namespace JwtAuth;

public class AgifyAgeDto
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("age")]
    public int Age { get; set; }
}
