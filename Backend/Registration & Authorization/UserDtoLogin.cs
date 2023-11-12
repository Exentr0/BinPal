namespace Backend.Models;

public class UserDtoLogin
{
    public required string Password { get; set; }
    public string Email { get; set; }
}