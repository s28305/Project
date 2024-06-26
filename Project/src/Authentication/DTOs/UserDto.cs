using System.ComponentModel.DataAnnotations;
using Project.Authentication.Models;

namespace Project.Authentication.DTOs;

public class UserDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(30, MinimumLength = 5, ErrorMessage = "Username should be between 5 and 30 characters")]
    public required string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(32, MinimumLength = 8, ErrorMessage = "Password should be between 8 and 32 characters")]
    [RegularExpression(@"^(?=.*[!@#$%^&*(),.?""{}|<>]).*$", ErrorMessage = "Password should contain at least one special character")]
    public required string Password { get; set; }

    public User Map()
    {
        return new User
        {
            Username = Username,
            Password = Password
        };
    }
}