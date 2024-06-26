namespace Project.Clients.Models;

public class Individual: Client
{
    public required string FirstName { get; set; } 
    public required string LastName { get; set; } 
    public required string Pesel { get; init; }
    public void Delete()
    {
        IsDeleted = true;
    }
}


