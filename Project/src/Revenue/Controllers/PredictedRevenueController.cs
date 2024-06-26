using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Project.Helpers;
using Project.Revenue.Services;

namespace Project.Revenue.Controllers;

[ApiController]
[Route("api/revenue/predicted")]
public class PredictedRevenueController(RevenueContext context, IRevenueService revenueService) : ControllerBase
{
    [Authorize]
    [HttpGet("predicted-total")]
    public ActionResult<double> GetPredictedTotalRevenue()
    {
        try
        {
            double totalRevenue = 0;

            totalRevenue += context.Contracts.Sum(c => c.Price);

            return Ok(totalRevenue);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("product/{softwareSystemId:int}")]
    public async Task<ActionResult<double>> GetPredictedRevenueForProduct(int softwareSystemId)
    {
        if (await revenueService.SoftwareExists(softwareSystemId))
        {
            return NotFound("Software system not found.");
        }
        
        try
        {
            double productRevenue = 0;

            productRevenue += context.Contracts
                .Where(c => c.SoftwareSystemId == softwareSystemId)
                .Sum(c => c.Price);

            return Ok(productRevenue);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    
    [Authorize]
    [HttpGet("total/{currency}")] 
    public async Task<ActionResult<double>> GetPredictedTotalRevenue(string currency)
    {
        try 
        {
            var exchangeRate = await GetExchangeRate(currency.ToUpper());
            var totalRevenuePln = context.Contracts.Sum(c => c.Price);
            var totalRevenue = totalRevenuePln * exchangeRate;
          
            return Ok(totalRevenue);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    
    [Authorize]
    [HttpGet("product/{softwareSystemId}/{currency}")]
    public async Task<ActionResult<double>> GetRevenueForProduct(int softwareSystemId, string currency)
    {
        if (await revenueService.SoftwareExists(softwareSystemId))
        {
            return NotFound("Software system not found.");
        }
        
        try 
        { 
            var exchangeRate = await GetExchangeRate(currency.ToUpper()); 
            var productRevenuePln = context.Contracts
                .Where(c => c.SoftwareSystemId == softwareSystemId)
                .Sum(c => c.Price);
            var productRevenue = productRevenuePln * exchangeRate;

            return Ok(productRevenue);
        }
        catch (Exception ex)
        { 
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    public static async Task<double> GetExchangeRate(string targetCurrency)
    { 
        var requestUrl = $"https://api.frankfurter.app/latest?from=PLN&to={targetCurrency}"; 
        
        using var client = new HttpClient();
        var response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var data = JObject.Parse(responseBody);
        return data["rates"][targetCurrency].Value<double>();
    }
}