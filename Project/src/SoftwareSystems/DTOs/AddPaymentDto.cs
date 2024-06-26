using Project.SoftwareSystems.Models;

namespace Project.SoftwareSystems.DTOs;

public class AddPaymentDto
{
    public double Amount { get; set; }
    
    public int ContractId { get; set; }
    
    public Payment Map()
    {
        return new Payment
        {
            PaymentDate = DateTime.UtcNow,
            ContractId = ContractId,
            Amount = Amount
        };
    }
}