using Microsoft.AspNetCore.Mvc;
using Tutorial5.Models;
using Tutorial5.Interfaces;

namespace Tutorial5.Controllers;

[ApiController]
[Route("api/animals")]
public class AnimalController(IAnimalRepository animalRepository) : ControllerBase
{
    [HttpGet]
    public Task<IEnumerable<Animal>> GetAll()
    {
        return animalRepository.GetAll();
    }
    

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var animal = animalRepository.GetById(id);
        return animal == null ? NotFound($"Animal with Id {id} not found.") : Ok(animal);
    }

    [HttpPost]
    public IActionResult Add(Animal animal)
    {
        animalRepository.Add(animal); 
        return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
    }
    
    [HttpPut]
    public IActionResult Edit(Animal animal)
    {
        var edited = animalRepository.Edit(animal);
    
        return !edited ? NotFound($"Animal with Id {animal.Id} not found.") : Ok();
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = animalRepository.Delete(id);
    
        if (!deleted)
        {
            return NotFound($"Animal with Id {id} not found.");
        }

        return NoContent();
    }
    
    [HttpGet("{animalId}/visits")]
    public IActionResult GetAllVisits(int animalId)
    {
        var animal = animalRepository.GetById(animalId);
        return animal == null ? NotFound($"Animal with Id {animalId} not found.") : Ok(animal.GetAllVisits());
    }
    
    [HttpPost("{animalId}/visits")]
    public IActionResult AddVisit(int animalId, Visit visit)
    {
        var animal = animalRepository.GetById(animalId);
        
        if (animal == null)
        {
            return NotFound($"Animal with Id {animalId} not found.");
        }
        
        animal.AddVisit(visit);
        return Ok();
    }
}