using System.ComponentModel.DataAnnotations;
using Project.SoftwareSystems.Models;

namespace Project.SoftwareSystems.DTOs;

public class AddContractDto
{
    [Required(ErrorMessage = "Start date is required.")]
    //[StringLength(11, ErrorMessage = "PESEL must be 11 digits.")]
    public DateTime StartDate { get; set; }
    
    [Required(ErrorMessage = "End date is required.")]
    public DateTime EndDate { get; set; }
    
    [Range(0, 3, ErrorMessage = "Support years must be between 1 and 3.")]
    public int SupportYears { get; set; }
    
    [Required(ErrorMessage = "Software system id is required.")]
    public int SoftwareSystemId { get; set; }
    
    [Required(ErrorMessage = "Software system version is required.")]
    public required string SoftwareVersion { get; set; }
    
    [Required(ErrorMessage = "Client id is required.")]
    public int ClientId { get; set; }

    public Contract Map(double upfrontCost)
    {
        return new Contract
        {
            StartDate = StartDate,
            EndDate = EndDate,
            Price = upfrontCost,
            SupportYears = SupportYears,
            SoftwareSystemId = SoftwareSystemId,
            SoftwareVersion = SoftwareVersion,
            ClientId = ClientId
        };
    }
}