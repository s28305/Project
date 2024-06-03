using System.Text.Json.Serialization;

namespace Tutorial6.DTO;

public class GetAnimalDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; set; }
    public required string Name { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }
    public required string AnimalType { get; set; }
}