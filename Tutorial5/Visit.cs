namespace Tutorial5;

public class Visit(DateTime date, string description, double price, Animal animal)
{
    public DateTime Date { get; set; } = date;
    public string Description { get; set; } = description;
    public double Price { get; set; } = price;
    public Animal Animal { get; set; } = animal;
}