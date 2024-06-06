using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial6.DTO;
using Tutorial6.Helpers;
using Tutorial6.Models;

namespace Tutorial6.Controllers
{
    [Route("api/visits")]
    [ApiController]
    public class VisitController(AnimalClinicContext context) : ControllerBase
    {
        // GET: api/Visit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetVisitDto>>> GetVisits()
        {
            try
            {
                var visits = await context.Visits
                    .Include(v => v.Employee)
                    .Include(v => v.Animal)
                    .OrderBy(v => v.Date)
                    .ToListAsync();

                return visits.Select(MapToVisitDtoWithNames).ToList();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error occurred while getting visits.", details = e.Message });
            }
        }

        // GET: api/Visit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetVisitDto>> GetVisit(int id)
        {
            try
            {
                var visit = await context.Visits
                    .Include(v => v.Employee)
                    .Include(v => v.Animal)
                    .FirstOrDefaultAsync(v => v.Id == id);

                if (visit == null)
                {
                    return NotFound($"Visit with Id {id} was not found.");
                }

                return MapToVisitDtoWithIds(visit);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error occurred while getting visit.", details = e.Message });
            }
        }

        // PUT: api/Visit/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisit(int id, [FromBody] string newDate)
        {
            try
            {
                if (!VisitExists(id))
                {
                    return NotFound($"Visit with Id {id} was not found.");
                }

                if (string.IsNullOrEmpty(newDate) || newDate.Length > 100)
                {
                    return BadRequest("Date should be provided and cannot exceed length of 100 characters.");
                }

                var currentVisit = await context.Visits.FirstAsync(v => v.Id == id);

                currentVisit.Date = newDate;

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
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error occurred while updating visit.", details = e.Message });
            }
        }

        // POST: api/Visit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetVisitDto>> PostVisit([FromBody] AddVisitDto visit)
        {
            try
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

                return CreatedAtAction("GetVisit", new { id = newVisit.Id }, MapToVisitDtoWithIds(newVisit));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error occurred while adding visit.", details = e.Message });
            }
        }

    // DELETE: api/Visit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            try
            {
                var visit = await context.Visits.FindAsync(id);
                if (visit == null)
                {
                    return NotFound($"Visit  with Id {id} was not found.");
                }

                context.Visits.Remove(visit);
                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error occurred while deleting visit.", details = e.Message });
            }
        }

        private bool VisitExists(int id)
        {
            return context.Visits.Any(e => e.Id == id);
        }
        
        private static GetVisitDto MapToVisitDtoWithNames(Visit visit)
        { 
            return new GetVisitDto
            {
                Id = visit.Id,
                Date = visit.Date,
                EmployeeName = visit.Employee.Name, 
                AnimalName = visit.Animal.Name   
            };
        }
        
        private static GetVisitDto MapToVisitDtoWithIds(Visit visit)
        { 
            return new GetVisitDto
            {
                Id = visit.Id,
                Date = visit.Date,
                EmployeeId = visit.Employee.Id, 
                AnimalId = visit.Animal.Id   
            };
        }
    }
}
