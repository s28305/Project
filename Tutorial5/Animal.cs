namespace Tutorial5;

public class Animal(int id, string name, string category, double weight, string furColor)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Category { get; set; } = category;
    public double Weight { get; set; } = weight;
    public string FurColor { get; set; } = furColor;
    private List<Visit> Visits { get; set; } = [];
    
    public List<Visit> GetAllVisits()
    {
        return Visits;
    }
    
    public void AddVisit(Visit visit)
    {
        Visits.Add(visit);
    }

}