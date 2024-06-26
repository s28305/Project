using System.ComponentModel.DataAnnotations;

namespace Project.SoftwareSystems.Models;

public class Payment
{
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public double Amount { get; set; }
    
    public int ContractId { get; set; }
    public Contract Contract { get; set; }
    
    [Timestamp]
    public byte[] ConcurrencyToken { get; set; }
}
