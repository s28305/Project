using Tutorial5.Interfaces;
using Tutorial5.Models;

namespace Tutorial5.Repositories;

public class AnimalRepository: IAnimalRepository
{
    private List<Animal> Animals { get; set; } = [];
    
    public Task<IEnumerable<Animal>> GetAll()
    {
        return Task.Run(() => Animals.AsEnumerable());
    }

    public Animal GetById(int id)
    {
        //As we don't have the pre-condition that id-s are in the strongly ascending order and unique,
        //it's better to iterate through list
        return Animals.FirstOrDefault(animal => animal.Id == id) ?? 
               throw new InvalidOperationException($"Animal with id {id} doesn't exist");
    }

    public void Add(Animal animal)
    {
        Animals.Add(animal);
    }

    public void Edit(Animal animal)
    {
        var index = Animals.FindIndex(a => a.Id == animal.Id);
        
        if (index != -1)
        {
            Animals[index] = animal;
        }
        else
        {
            throw new InvalidOperationException($"Animal with id {animal.Id} doesn't exist");
        }
    }

    public bool Delete(int id)
    {
        return Animals.Remove(GetById(id));
    }
}