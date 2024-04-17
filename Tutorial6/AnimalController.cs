using Microsoft.AspNetCore.Mvc;

namespace Tutorial6;

[ApiController]
[Route("api/animals")]
public class AnimalController(IAnimalRepository animalRepository) : ControllerBase
{

        [HttpGet]
        public IActionResult GetAnimals(string orderBy = "Name")
        {
            if (!IsValidOrderBy(orderBy.ToLower()))
            {
                return BadRequest("Incorrect orderBy parameter. " +
                                  "Possible values include: name, description, category and area.");
            }

            var sorted = animalRepository.GetAll(orderBy);

            return Ok(sorted);
        }
        
        private static bool IsValidOrderBy(string orderBy)
        {
            var parameters = new[] { "name", "description", "category", "area" };
            return parameters.Contains(orderBy);
        }
        
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var animal = animalRepository.GetById(id);
            return animal == null ? NotFound($"Animal with Id {id} was not found.") : Ok(animal);
        }
        
        [HttpPost]
        public IActionResult Add(Animal animal)
        {
            animalRepository.Add(animal); 
            return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
        }
    }