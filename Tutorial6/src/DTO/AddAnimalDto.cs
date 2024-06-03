using System.ComponentModel.DataAnnotations;

namespace Tutorial6.DTO;

public class AddAnimalDto
{
    [Required]
    [Length(1, 100)]
    public required string Name { get; set; }

    [Length(0, 2000)]
    public string? Description { get; set; }

    [Required]
    public int AnimalTypesId { get; set; }
}