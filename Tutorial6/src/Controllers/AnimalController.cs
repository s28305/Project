using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial6.DTO;
using Tutorial6.Helpers;
using Tutorial6.Models;

namespace Tutorial6.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalController(AnimalContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals(string orderBy = "Name")
        {
            if (!IsValidOrderBy(orderBy.ToLower()))
            {
                return BadRequest("Incorrect orderBy parameter. " +
                                  "Possible values include: name, description, category and area.");
            }
            
            var sorted = orderBy.ToLower() switch
            {
                "description" => context.Animals.OrderBy(a => a.Description),
                _ => context.Animals.OrderBy(a => a.Name)
            };
            
            return await sorted.ToListAsync();
        }
        
        private static bool IsValidOrderBy(string orderBy)
        {
            var parameters = new[] { "name", "description" };
            return parameters.Contains(orderBy);
        } 
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await context.Animals.FindAsync(id);

            return animal == null ? NotFound($"Animal with Id {id} was not found.") : Ok(animal);
        }
        
        // Animal Id is auto-generated (I changed that so it's more convenient)
        [HttpPost]
        public async Task<ActionResult<Animal>> AddAnimal(AnimalDto animalDto)
        {
            var animalType = await context.Animals.FindAsync(animalDto.AnimalTypesId);
            if (animalType == null)
            {
                return BadRequest("Animal with given animal type does not exist");
            }
            
            var animal = ConvertDtoToAnimal(animalDto);
            context.Animals.Add(animal);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        private static Animal ConvertDtoToAnimal(AnimalDto animalDto)
        {
            return new Animal
            {
                Name = animalDto.Name,
                Description = string.IsNullOrEmpty(animalDto.Description) ? null : animalDto.Description,
                AnimalTypesId = animalDto.AnimalTypesId
            };
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Animal>> UpdateAnimal(int id, AnimalDto animalDto)
        {
            var animal = await context.Animals.FindAsync(id);

            if (animal == null)
            {
                return await AddAnimal(animalDto);
            }
            
            var animalType = await context.Animals.FindAsync(animalDto.AnimalTypesId);
            if (animalType == null)
            {
                return BadRequest("Animal with given animal type does not exist");
            }
           
            animal.Name = animalDto.Name;
            animal.Description = string.IsNullOrEmpty(animalDto.Description) ? null : animalDto.Description;
            animal.AnimalTypesId = animalDto.AnimalTypesId;

            context.Entry(animal).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return NoContent();
        }
        
        
        // I decided to add delete endpoint as you accidentally skipped that part in the description :)
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await context.Animals.FindAsync(id);
            
            if (animal == null)
            {
                return NotFound($"Animal with Id {id} was not found.");
            }

            context.Animals.Remove(animal);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
