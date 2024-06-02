namespace Tutorial6.DTO;

public class GetAnimalDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string AnimalType { get; set; }
}