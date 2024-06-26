using Project.SoftwareSystems.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Helpers;
using Project.SoftwareSystems.DTOs;

namespace Project.SoftwareSystems.Controllers;

[Route("api/contracts")]
[ApiController]
public class ContractController(RevenueContext context) : ControllerBase
{
    // POST: api/contracts
    [HttpPost]
    public async Task<ActionResult<Contract>> CreateContract(AddContractDto contractDto)
    {
        var softwareSystem = await context.SoftwareSystems
            .FirstOrDefaultAsync(s => s.Id == contractDto.SoftwareSystemId);

        if (softwareSystem == null)
        {
            return NotFound("Software system not found.");
        }
        if (!ValidDuration(contractDto.StartDate, contractDto.EndDate))
        {
            return BadRequest("Contract duration must be between 3 and 30 days.");
        }

        if (await HasActiveContract(contractDto.SoftwareSystemId))
        {
            return BadRequest("Client already has an active contract for the software system.");
        }
        
        var contract = contractDto.Map(softwareSystem.UpfrontCost);
        var totalPrice = GetTotalPrice(contract);
        contract.Price = totalPrice;

        context.Contracts.Add(contract);
        await context.SaveChangesAsync();

        return CreatedAtAction("", new { id = contract.Id }, contract);
    }

    private static bool ValidDuration(DateTime startDate, DateTime endDate)
    {
        var contractDuration = (endDate - startDate).Days;
        return contractDuration is >= 3 and <= 30 && startDate < endDate;
    }

    private async Task<bool> HasActiveContract(int softwareId)
    {
        return await context.Contracts
            .AnyAsync(c => c.SoftwareSystemId == softwareId && c.IsSigned && !c.IsCancelled);
    }

    private double GetTotalPrice(Contract contract)
    {
        // upfront cost with discounts
        var totalPrice = GetDiscount(contract.Price, contract.SoftwareSystemId, contract.ClientId);
        
        // support costs
        totalPrice += contract.SupportYears * 1000;

        return totalPrice;
    }

    private double GetDiscount(double totalPrice, int softwareSystemId, int clientId)
    {
        var availableDiscount = context.Discounts
            .Where(d => d.SoftwareSystemId == softwareSystemId
                        && d.StartDate <= DateTime.Today
                        && d.EndDate >= DateTime.Today)
            .OrderByDescending(d => d.Value)
            .FirstOrDefault();

        if (availableDiscount != null)
        {
            totalPrice *= 1 - availableDiscount.Value * 0.01;

        }
        
        if (IsReturning(clientId))
        {
            totalPrice *= 0.95; 
        }

        return totalPrice;
    }

    private bool IsReturning(int clientId)
    {
        return context.Contracts.Any(c => c.ClientId == clientId && (c.IsSigned || !c.IsCancelled));
    }


    // POST: api/contracts/payments/{id}
    [HttpPost("payments/{id}")]
    public async Task<ActionResult<Payment>> IssuePayment(int id, Payment payment)
    {
        var contract = await context.Contracts
            .Include(c => c.Payments) 
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (contract == null)
        {
            return NotFound($"Contract with id {id} was not found.");
        }

        if (contract.IsCancelled)
        {
            return BadRequest("Contract was cancelled.");
        }

        if (contract.IsSigned && DateTime.UtcNow > contract.EndDate)
        { 
            contract.Cancel();
            return BadRequest("Contract payment period has expired.");
        }
        
        var remainingPrice = contract.Price - contract.Payments.Sum(p => p.Amount);

        if (payment.Amount <= 0 || payment.Amount > remainingPrice)
        {
            return BadRequest($"Payment amount should be between 0 and {remainingPrice}.");
        }

        payment.Map(id);
        context.Payments.Add(payment);
        await context.SaveChangesAsync();
        
        if (contract.Price < contract.Payments.Sum(p => p.Amount))
            return CreatedAtAction("", new { id = payment.Id }, payment);
        
        contract.IsSigned = true;
        context.Entry(contract).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        return CreatedAtAction("", new { id = payment.Id }, new
        { 
            Payment = payment,
            Message = "Contract was fully paid."
        });
    }
    
    ///////////////////////////////////
    
    // GET: api/contracts/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Contract>> GetContract(int id)
    {
        var contract = await context.Contracts.FindAsync(id);

        if (contract == null)
        {
            return NotFound($"Contract with id {id} not found.");
        }

        return contract;
    }

    // GET: api/contracts/{id}/payments
    [HttpGet("{id}/payments")]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentsForContract(int id)
    {
        var contract = await context.Contracts.FindAsync(id);

        if (contract == null)
        {
            return NotFound($"Contract with id {id} not found.");
        }

        return contract.Payments.ToList();
    }

}