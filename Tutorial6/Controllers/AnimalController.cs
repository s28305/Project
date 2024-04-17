using Microsoft.AspNetCore.Mvc;
using Tutorial6.Models;
using Tutorial6.Repositories;

namespace Tutorial6.Controllers;

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
            if (!animalRepository.IdNotExists(animal.Id))
                return BadRequest($"Animal with the Id {animal.Id} already exists.");
            animalRepository.Add(animal);
            return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);

        }
        
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest($"Id {id} doesn't correspond to animal id {animal.Id}.");
            }

            var exists = animalRepository.IdNotExists(id);
            
            if (exists)
            { 
                animalRepository.Add(animal);
                return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal); 
            }
            var updated = animalRepository.Update(animal);
            return updated ? Ok() : BadRequest("Error updating data.");
        }

    }