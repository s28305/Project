using Project.Clients.Models;

namespace Project.Clients.DTOs;

public class PutIndividualDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Individual Map()
    {
        return new Individual
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            Address = Address,
            Email = Email,
            PhoneNumber = PhoneNumber
        };
    }
}