using System.ComponentModel.DataAnnotations;

namespace Tutorial6.Models;

public class AnimalTypes
{
    public int Id { get; set; }

    [Required]
    [Length(1, 150)]
    public required string Name { get; set; }
}