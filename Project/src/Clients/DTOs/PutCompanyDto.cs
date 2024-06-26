using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using Project.Clients.Models;

namespace Project.Clients.DTOs;

public class PutCompanyDto
{
    
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Company name is required.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Company name must be between 1 and 50 characters.")]
    public required string CompanyName { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Address must be between 1 and 100 characters.")]
    public required string Address { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [StringLength(25, MinimumLength = 1, ErrorMessage = "Email must be between 1 and 25 characters.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [StringLength(10, ErrorMessage = "Phone number must not exceed 10 characters.")]
    public required string PhoneNumber { get; set; }

    public void Map(Company company)
    {
        company.CompanyName = CompanyName;
        company.Address = Address;
        company.Email = Email;
        company.PhoneNumber = PhoneNumber;
    }
}