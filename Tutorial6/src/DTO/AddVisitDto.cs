namespace Tutorial6.DTO;

public class AddVisitDto
{
    public int EmployeeId { get; set; }
    public int AnimalId { get; set; }
    public required string Date { get; set; }
}