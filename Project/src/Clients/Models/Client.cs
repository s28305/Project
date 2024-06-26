namespace Project.Clients.Models
{
    public abstract class Client
    {
        public int Id { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
    }
}
