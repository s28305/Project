using Microsoft.AspNetCore.Mvc;
using Tutorial5.Properties.Interfaces;

namespace Tutorial5.Properties.Classes;

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
    public Animal GetById(int id)
    {
        return animalRepository.GetById(id);
    }

    [HttpPost]
    public void Add(Animal animal)
    {
        animalRepository.Add(animal);
    }
    
    [HttpPut]
    public void Edit(Animal animal)
    {
        animalRepository.Edit(animal);
    }
    
    [HttpDelete]
    public bool Delete(int id)
    {
        return animalRepository.Delete(id);
    }
    
    [HttpGet("{animalId}/visits")]
    public List<Visit> GetAllVisits(int animalId)
    {
        return animalRepository.GetById(animalId).GetAllVisits();
    }
    
    [HttpPost("{animalId}/visits")]
    public void AddVisit(int animalId, Visit visit)
    {
        animalRepository.GetById(animalId).AddVisit(visit);
    }
}