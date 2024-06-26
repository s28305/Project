using System.ComponentModel.DataAnnotations;
using Project.Clients.Models;

namespace Project.Clients.DTOs;

public class AddIndividualDto
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 50 characters.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 50 characters.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Address must be between 1 and 100 characters.")]
    public required string Address { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [StringLength(25, MinimumLength = 1, ErrorMessage = "Email must be between 1 and 25 characters.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [StringLength(10, ErrorMessage = "Phone number must be 10 digits.")]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "PESEL is required.")]
    [StringLength(11, ErrorMessage = "PESEL must be 11 digits.")]
    public string? Pesel { get; set; }
    
    public Individual Map()
    {
        return new Individual
        {
            FirstName = FirstName,
            LastName = LastName,
            Address = Address,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Pesel = Pesel
        };
    }
}