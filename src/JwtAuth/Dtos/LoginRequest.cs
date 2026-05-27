namespace JwtAuth;

public class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class GenerateTokenRequest
{
    public string Username { get; set; } = null!;
    public int ExpirationMinutes { get; set; } = 60;
}

public class TokenResponse
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public string Username { get; set; } = null!;
}
