using Tutorial6.Models;

namespace Tutorial6.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAll(string orderBy);
    Animal? GetById(int id);
    bool Add(Animal animal);
    bool IdNotExists(int id);
    bool Update(Animal animal);
}