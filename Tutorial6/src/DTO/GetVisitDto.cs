namespace Tutorial6.DTO;

public class GetVisitDto
{
    public int Id { get; set; }
    public required string EmployeeName { get; set; }
    public required string AnimalName { get; set; }
    public required string Date { get; set; }
}