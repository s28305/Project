using System.ComponentModel.DataAnnotations;

namespace Tutorial6.DTO;

public class AddVisitDto
{
    [Required]
    public int EmployeeId { get; set; }
    
    [Required]
    public int AnimalId { get; set; }
    
    [Required]
    [Length(1, 100)]
    public required string Date { get; set; }
}