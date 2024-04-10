using Tutorial5.Properties.Classes;

namespace Tutorial5.Properties.Interfaces;

public interface IAnimalRepository
{
    Task<IEnumerable<Animal>> GetAll();
    Animal GetById(int id);
    void Add(Animal animal);
    void Edit(Animal animal);
    bool Delete(int id);
}