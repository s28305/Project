using System.ComponentModel.DataAnnotations;
using Project.Clients.Models;

namespace Project.Clients.DTOs;

public class AddCompanyDto
{
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

    [Required(ErrorMessage = "KRS is required.")]
    [StringLength(14, MinimumLength = 9, ErrorMessage = "KRS must be between 9 and 14 characters.")]
    public required string Krs { get; set; }

    public Company Map()
    {
        return new Company
        {
            CompanyName = CompanyName,
            Address = Address,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Krs = Krs
        };
    }
}