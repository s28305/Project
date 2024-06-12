using System.ComponentModel.DataAnnotations;

namespace Tutorial6.DTO;

public class LoginUserDto
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}