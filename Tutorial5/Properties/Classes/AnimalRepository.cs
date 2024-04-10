namespace Tutorial5;

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
        Animals[animal.Id] = animal;
    }

    public bool Delete(int id)
    {
        return Animals.Remove(GetById(id));
    }
}