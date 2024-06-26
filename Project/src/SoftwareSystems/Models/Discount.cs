namespace Project.SoftwareSystems.Models;

public class Discount
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Offer { get; set; }
    public int Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
