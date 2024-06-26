using Project.Clients.Models;

namespace Project.Clients.DTOs;

public class AddIndividualDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Pesel { get; set; }
    
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