using System.Text.Json.Serialization;

namespace Tutorial6.DTO;

public class GetVisitDto
{
    public int Id { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? EmployeeId { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? AnimalId { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EmployeeName { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public string? AnimalName { get; set; }
    public required string Date { get; set; }
}