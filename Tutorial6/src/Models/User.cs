namespace Tutorial6.Models;

public class User
{
    public int Id { get; set; }
    
    public required string Username { get; set; }
    public required string Password { get; set; }
    
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpire { get; set; }
    
    public int TypeId { get; set; }
    public UserType Type { get; set; }
}