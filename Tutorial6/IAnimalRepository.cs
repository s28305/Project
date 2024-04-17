namespace Tutorial6;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAll(string orderBy);
    Animal? GetById(int id);
    bool Add(Animal animal);
    bool Edit(Animal animal);
}