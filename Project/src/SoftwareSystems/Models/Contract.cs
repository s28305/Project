using System.ComponentModel.DataAnnotations;
using Project.Clients.Models;

namespace Project.SoftwareSystems.Models;

public class Contract
{
    public int Id { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public double Price { get; set; }
    public int SupportYears { get; init; }
    public bool IsSigned { get; set; }
    public bool IsCancelled { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    
    public int SoftwareSystemId { get; init; }
    public required string SoftwareVersion { get; init; }
    public SoftwareSystem SoftwareSystem { get; init; }
    
    public int ClientId { get; init; }
    public Client Client { get; init; }
    
    [Timestamp]
    public byte[] ConcurrencyToken { get; set; }
    
    public void Cancel()
    {
        if (!IsCancelled)
        {
            IsCancelled = true;
        }
    }
}