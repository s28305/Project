using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Clients.DTOs;
using Project.Helpers;

namespace Project.Clients.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController(RevenueContext context) : ControllerBase
    {
        // PUT: api/clients/companies
        [HttpPut("companies")]
        public async Task<IActionResult> PutCompany(PutCompanyDto updatedCompany, CancellationToken cancellationToken)
        {
            var existingCompany = await context.Companies.FindAsync([updatedCompany.Id], cancellationToken: cancellationToken);
            
            if (existingCompany == null)
            {
                return NotFound($"Company with Id {updatedCompany.Id} was not found.");
            }
            
            updatedCompany.Map(existingCompany);
            context.Entry(existingCompany).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(updatedCompany.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }


        // POST: api/clients/companies
        [HttpPost("companies")]
        public async Task<ActionResult<AddCompanyDto>> PostCompany(AddCompanyDto companyDto)
        {
            var company = companyDto.Map();
            context.Companies.Add(company);
            await context.SaveChangesAsync();

            return CreatedAtAction("", new { id = company.Id }, companyDto);
        }

        // PUT: api/clients/individuals
        [HttpPut("individuals")]
        public async Task<IActionResult> PutIndividual(PutIndividualDto updatedIndividual, CancellationToken cancellationToken)
        {
            var individual = await context.Individuals.FindAsync([updatedIndividual.Id], cancellationToken: cancellationToken);

            if (individual == null)
            {
                return BadRequest($"Individual client with Id {updatedIndividual.Id} was not found.");
            }

            updatedIndividual.Map(individual);
            context.Entry(individual).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndividualExists(updatedIndividual.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/clients/individuals
        [HttpPost("individuals")]
        public async Task<ActionResult<AddIndividualDto>> PostIndividual(AddIndividualDto individualDto)
        {
            var individual = individualDto.Map();
            context.Individuals.Add(individual);
            await context.SaveChangesAsync();

            return CreatedAtAction("", new { id = individual.Id }, individualDto);
        }

        // DELETE: api/clients/individuals/5
        [HttpDelete("individuals/{id:int}")]
        public async Task<IActionResult> DeleteIndividual(int id)
        {
            var individual = await context.Individuals.FindAsync(id);
            
            if (individual == null)
            {
                return NotFound($"Individual client with Id {id} was not found.");
            }
            
            individual.Delete(); 
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(int id)
        {
            return context.Companies.Any(e => e.Id.Equals(id));
        }

        private bool IndividualExists(int id)
        {
            return context.Individuals.Any(e => e.Id.Equals(id));
        }
    }
}
