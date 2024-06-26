using System.ComponentModel.DataAnnotations;
using Project.SoftwareSystems.Models;

namespace Project.SoftwareSystems.DTOs;

public class AddContractDto
{
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    [Required]
    public int SupportYears { get; set; }
    
    [Required]
    public int SoftwareSystemId { get; set; }

    public Contract Map()
    {
        return new Contract
        {
            StartDate = StartDate,
            EndDate = EndDate,
            Price = Price,
            SupportYears = SupportYears,
            SoftwareSystemId = SoftwareSystemId
        };
    }
}