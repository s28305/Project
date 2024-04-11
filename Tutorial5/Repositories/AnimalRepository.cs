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

    public Animal? GetById(int id)
    {
        //As we don't have the pre-condition that id-s are in the strongly ascending order and unique,
        //it's better to iterate through list
        return Animals.FirstOrDefault(animal => animal.Id == id);
    }

    public void Add(Animal animal)
    {
        Animals.Add(animal);
    }

    public bool Edit(Animal animal)
    {
        var index = Animals.FindIndex(a => a.Id == animal.Id);

        if (index == -1) return false;
        
        Animals[index] = animal;
        return true;

    }

    public bool Delete(int id)
    {
        var animal = GetById(id);

        if (animal == null) return false;
        
        Animals.Remove(animal);
        return true;
    }
}