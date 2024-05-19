using System.ComponentModel.DataAnnotations;

namespace Tutorial6.Models;

public class Animal
{
    public int Id { get; set; }
    
    [Required]
    [Length(1, 200)]
    public required string Name { get; set; }

    [Length(0, 200)]
    public string? Description { get; set; }

    [Required]
    [Length(1, 200)]
    public required string Category { get; set; }

    [Required]
    [Length(1, 200)]
    public required string Area { get; set; }
}