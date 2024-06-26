namespace Project.Revenue.Services;

public interface IRevenueService
{ 
    Task<bool> SoftwareExists(int softwareSystemId);
}