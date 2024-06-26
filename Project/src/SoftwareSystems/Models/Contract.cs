namespace Project.SoftwareSystems.Models;

public class Contract
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Price { get; set; }
    public int SupportYears { get; set; }
    public bool IsSigned { get; set; }
    public bool IsCancelled { get; set; }
    public ICollection<Payment> Payments { get; set; }
    
    public int SoftwareSystemId { get; set; }
    public SoftwareSystem SoftwareSystem { get; set; }
    
    public int DiscountId { get; set; }
    public Discount Discount { get; set; }
}