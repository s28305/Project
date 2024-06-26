using System.ComponentModel.DataAnnotations;

namespace Project.Authentication.DTOs;

public class LoginUserDto
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}