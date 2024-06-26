using System.ComponentModel.DataAnnotations;

namespace Project.Authentication.Models;

public class Role
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name should be between 5 and 50 characters")]
    public required string Name { get; set; }
}