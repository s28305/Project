using System.ComponentModel.DataAnnotations;

namespace Project.SoftwareSystems.Models;

public class SoftwareSystem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Version { get; set; }
    public required string Category { get; set; } 
    public bool HasSubscription { get; set; }
    public double UpfrontCost { get; set; }
    
    public int? DiscountId { get; set; }
    public Discount? Discount { get; set; }
    
    [Timestamp]
    public byte[] ConcurrencyToken { get; set; }
}
