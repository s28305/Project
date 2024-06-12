using System.ComponentModel.DataAnnotations;

namespace Tutorial6.Models;

public class UserType
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name should be between 5 and 50 characters")]
    public required string Name { get; set; }
}