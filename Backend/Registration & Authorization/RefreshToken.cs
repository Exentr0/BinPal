namespace Backend.Registration___Authorization;

public class RefreshToken
{
    public required string Token { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ExpiresAt { get; set; }
}