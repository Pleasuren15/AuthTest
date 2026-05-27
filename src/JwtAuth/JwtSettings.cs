using System.Text.Json.Serialization;

namespace JwtAuth;

public class JwtSettings
{
    [JsonPropertyName("secretKey")]
    public string SecretKey { get; set; } = null!;

    [JsonPropertyName("issuer")]
    public string Issuer { get; set; } = null!;

    [JsonPropertyName("audience")]
    public string Audience { get; set; } = null!;

    [JsonPropertyName("expirationMinutes")]
    public int ExpirationMinutes { get; set; }
}
