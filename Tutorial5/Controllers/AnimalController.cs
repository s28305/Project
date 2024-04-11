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
    public void Add(Animal animal)
    {
        animalRepository.Add(animal);
    }
    
    [HttpPut]
    public IActionResult Edit(Animal animal)
    {
        var edited = animalRepository.Edit(animal);
    
        return !edited ? NotFound($"Animal with Id {animal.Id} not found.") : Ok();
    }
    
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var deleted = animalRepository.Delete(id);
    
        return !deleted ? NotFound($"Animal with Id {id} not found.") : Ok();
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