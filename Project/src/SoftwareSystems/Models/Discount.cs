using System.ComponentModel.DataAnnotations;

namespace Project.SoftwareSystems.Models;

public class Discount
{
    public int Id { get; set; }
    
    [Required]
    [Length(1, 50)]
    public string Name { get; set; }
    
    [Required]
    [Length(1, 50)]
    public string Offer { get; set; }
    public int Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int SoftwareSystemId { get; set; }
    public SoftwareSystem SoftwareSystem { get; set; }
    
    [Timestamp]
    public byte[] ConcurrencyToken { get; set; }
    
    
}
