using System.ComponentModel.DataAnnotations;

namespace Tutorial6.DTO;

public class UpdateAnimalDto
{
    [Required]
    [Length(1, 100)]
    public required string Name { get; set; }

    [Length(0, 2000)]
    public string? Description { get; set; }
}