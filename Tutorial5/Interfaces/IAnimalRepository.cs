using Tutorial5.Models;

namespace Tutorial5.Interfaces;

public interface IAnimalRepository
{
    Task<IEnumerable<Animal>> GetAll();
    Animal GetById(int id);
    void Add(Animal animal);
    void Edit(Animal animal);
    bool Delete(int id);
}