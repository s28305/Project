using Microsoft.EntityFrameworkCore;
using Project.Helpers;

namespace Project.Revenue.Services;

public class RevenueService(RevenueContext context): IRevenueService
{
    public async Task<bool> SoftwareExists(int softwareSystemId)
    {
        var softwareSystem = await context.SoftwareSystems
            .FirstOrDefaultAsync(s => s.Id == softwareSystemId);

        return softwareSystem == null;
    }
}