using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial6.DTO;
using Tutorial6.Helpers;
using Tutorial6.Models;

namespace Tutorial6.Controllers
{
    [Route("api/visits")]
    [ApiController]
    public class VisitController(VisitContext context) : ControllerBase
    {
        // GET: api/Visit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetVisitDto>>> GetVisits()
        {
            var visits = await context.Visits
                .Include(v => v.Employee) 
                .Include(v => v.Animal) 
                .OrderBy(v => v.Date)
                .ToListAsync();

            return visits.Select(MapToVisitDto).ToList();
        }

        // GET: api/Visit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visit>> GetVisit(int id)
        {
            var visit = await context.Visits
                .Include(v => v.Employee) 
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (visit == null)
            {
                return NotFound();
            }

            return visit;
        }

        // PUT: api/Visit/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisit(int id, AddVisitDto visitDto)
        {
            if (!VisitExists(id))
            {
                return NotFound();
            }

            var currentVisit = await context.Visits.FirstAsync(v => v.Id == id);
            
            currentVisit.EmployeeId = visitDto.EmployeeId;
            currentVisit.AnimalId = visitDto.AnimalId;
            currentVisit.Date = visitDto.Date;
            
            context.Entry(currentVisit).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Visit with given Id is already being modified by another user");
            }

            return NoContent();
        }

        // POST: api/Visit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Visit>> PostVisit([FromBody] AddVisitDto visit)
        {
            var emp = await context.Employees.FindAsync(visit.EmployeeId);
            if (emp == null)
            {
                return BadRequest("Employee with given Id does not exist");
            }
            
            var animal = await context.Animals.FindAsync(visit.AnimalId);
            if (animal == null)
            {
                return BadRequest("Animal with given Id does not exist");
            }
            
            var newVisit = new Visit
            {
                AnimalId = visit.AnimalId,
                EmployeeId = visit.EmployeeId,
                Date = visit.Date
            };
            context.Visits.Add(newVisit);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetVisit", new { id = newVisit.Id }, newVisit);
        }

        // DELETE: api/Visit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            var visit = await context.Visits.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }

            context.Visits.Remove(visit);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitExists(int id)
        {
            return context.Visits.Any(e => e.Id == id);
        }
        
        private static GetVisitDto MapToVisitDto(Visit visit)
        { 
            return new GetVisitDto
            {
                Id = visit.Id,
                Date = visit.Date,
                EmployeeName = visit.Employee.Name, 
                AnimalName = visit.Animal.Name   
            };
        }
    }
}
