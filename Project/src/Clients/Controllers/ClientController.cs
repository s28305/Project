using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Clients.DTOs;
using Project.Clients.Helpers;
using Project.Clients.Models;

namespace Project.Clients.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController(ClientContext context) : ControllerBase
    {
        // GET: api/clients/companies
        [HttpGet("companies")]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = await context.Companies.ToListAsync();
            return companies;
        }

        // GET: api/clients/companies/5
        [HttpGet("companies/{id:int}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound($"Company with Id {id} was not found.");
            }

            return company;
        }

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
        public async Task<ActionResult<Company>> PostCompany(AddCompanyDto companyDto)
        {
            var company = companyDto.Map();
            context.Companies.Add(company);
            await context.SaveChangesAsync();

            return CreatedAtAction("", new { id = company.Id }, companyDto);
        }

        // GET: api/clients/individuals
        [HttpGet("individuals")]
        public async Task<ActionResult<IEnumerable<Individual>>> GetIndividuals()
        {
            var individuals = await context.Individuals.ToListAsync();
            return individuals;
        }

        // GET: api/clients/individuals/5
        [HttpGet("individuals/{id:int}")]
        public async Task<ActionResult<Individual>> GetIndividual(int id)
        {
            var individual = await context.Individuals.FindAsync(id);

            if (individual == null)
            {
                return NotFound($"Individual client with Id {id} was not found.");
            }

            return individual;
        }

        // PUT: api/clients/individuals
        [HttpPut("individuals")]
        public async Task<IActionResult> PutIndividual(PutIndividualDto updatedIndividual, CancellationToken cancellationToken)
        {
            var individual = await context.Individuals.FindAsync(new object?[] { updatedIndividual.Id }, cancellationToken: cancellationToken);

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
        public async Task<ActionResult<Individual>> PostIndividual(AddIndividualDto individualDto)
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
            
            individual.IsDeleted = true; 
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(int id)
        {
            return context.Companies.Any(e => e.Id == id);
        }

        private bool IndividualExists(int id)
        {
            return context.Individuals.Any(e => e.Id == id);
        }
    }
}
