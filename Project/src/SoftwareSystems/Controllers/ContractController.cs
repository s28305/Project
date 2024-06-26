using Project.Clients.Helpers;
using Project.SoftwareSystems.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.SoftwareSystems.DTOs;

namespace Project.SoftwareSystems.Controllers
{
    [Route("api/contracts")]
    [ApiController]
    public class ContractController(ClientContext context) : ControllerBase
    {
        // POST: api/contracts
        [HttpPost]
        public async Task<ActionResult<Contract>> CreateContract(AddContractDto contractDto)
        {
            if (!ValidDuration(contractDto.StartDate, contractDto.EndDate))
            {
                return BadRequest("Contract duration must be between 3 and 30 days.");
            }
            
            if (await HasActiveContract(contractDto.SoftwareSystemId))
            {
                return BadRequest("Client already has an active contract for the software system.");
            }
            
            var contract = contractDto.Map();
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
        
        private static double GetTotalPrice(Contract contract)
        {
            // Dummy calculation, adjust based on your business logic
            double totalPrice = contract.Price; // Start with base price

            // Apply discounts if applicable
            /*    if (contract.DiscountId.HasValue)
                {
                    var discount = context.Discounts.FirstOrDefault(d => d.Id == contract.DiscountId);
                    if (discount != null)
                    {
                        totalPrice *= (1 - (discount.Percentage / 100));
                    }
                }

                // Apply returning client discount if applicable
                if (IsReturningClient(contract.ClientId))
                {
                    totalPrice *= 0.95; // 5% discount for returning clients
                }
                */

            // Additional support costs
            totalPrice += contract.SupportYears * 1000;

            return totalPrice;
        }

        /*   // POST: api/contracts/{id}/payments
        [HttpPost("{id}/payments")]
        public async Task<ActionResult<Payment>> IssuePayment(int id, Payment payment)
        {
            var contract = await context.Contracts.FindAsync(id);

            if (contract == null)
            {
                return NotFound($"Contract with id {id} not found.");
            }

            if (contract.IsCancelled)
            {
                return BadRequest("Cannot issue payment for a cancelled contract.");
            }

            if (contract.IsSigned && DateTime.UtcNow > contract.EndDate)
            {
                return BadRequest("Contract payment period has expired.");
            }

            // Calculate remaining amount to be paid
            double remainingAmount = (double)(contract.Price - contract.Payments.Sum(p => p.Amount));

            if (payment.Amount <= 0 || payment.Amount > remainingAmount)
            {
                return BadRequest($"Invalid payment amount. Amount should be between 0 and {remainingAmount}.");
            }

            payment.PaymentDate = DateTime.UtcNow;
            payment.ContractId = id;

            context.Payments.Add(payment);
            await context.SaveChangesAsync();

            // Check if total payments equal contract price, mark contract as signed if so
            if (contract.Price == contract.Payments.Sum(p => p.Amount))
            {
                contract.IsSigned = true;
                context.Entry(contract).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }
        */

    /*    private bool IsReturningClient(int clientId)
        {
            // Check if client has any previous contracts or subscriptions
            return context.Contracts.Any(c => c.ClientId == clientId && (c.IsSigned || !c.IsCancelled))
                   || context.Subscriptions.Any(s => s.ClientId == clientId && (s.IsActive || !s.IsCancelled));
        }

        private bool IsSoftwareSystemAvailableForContract(int softwareSystemId)
        {
            // Check if software system is available for contract creation
            return context.SoftwareSystems.Any(ss => ss.Id == softwareSystemId && ss.AvailableForContract);
        }

        private bool IsPaymentPeriodValid(Contract contract)
        {
            // Check if current date is within the payment period specified in the contract
            return DateTime.UtcNow >= contract.StartDate && DateTime.UtcNow <= contract.EndDate;
        }

        private async Task<bool> IsContractFullyPaid(int contractId)
        {
            var contract = await context.Contracts.FindAsync(contractId);
            if (contract != null)
            {
                decimal totalPayments = contract.Payments.Sum(p => p.Amount);
                return totalPayments >= (decimal)contract.Price;
            }

            return false;
        }

        private async Task<bool> IsContractCancelled(int contractId)
        {
            var contract = await context.Contracts.FindAsync(contractId);
            return contract != null && contract.IsCancelled;
        }

        private async Task<bool> IsContractSigned(int contractId)
        {
            var contract = await context.Contracts.FindAsync(contractId);
            return contract != null && contract.IsSigned;
        }

        private async Task<bool> IsContractExpired(int contractId)
        {
            var contract = await context.Contracts.FindAsync(contractId);
            return contract != null && DateTime.UtcNow > contract.EndDate;
        }

        private async Task<bool> IsContractPaymentPeriodExpired(int contractId)
        {
            var contract = await context.Contracts.FindAsync(contractId);
            return contract != null && DateTime.UtcNow > contract.EndDate;
        }

        private async Task<bool> IsClientEligibleForContract(int clientId, int softwareSystemId)
        {
            // Additional eligibility checks for client
            var client = await context.Clients.FindAsync(clientId);
            if (client != null)
            {
                // Example: Client must meet certain criteria to be eligible for the contract
                // Implement your business logic here
                return true; // Placeholder
            }

            return false;
        }

        private async Task<bool> IsClientActive(int clientId)
        {
            var client = await context.Clients.FindAsync(clientId);
            return client != null && client.IsActive;
        }

        private async Task<bool> IsSoftwareSystemAvailable(int softwareSystemId)
        {
            var softwareSystem = await context.SoftwareSystems.FindAsync(softwareSystemId);
            return softwareSystem != null && softwareSystem.IsAvailable;
        }
        */

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
}
