namespace Tutorial6;

using System.ComponentModel.DataAnnotations;

public class Animal
{
    public int Id { get; set; }
    
    [Required]
    [Length(1, 200)]
    public string Name { get; set; }
    
    [Length(0, 200)]
    public string Description { get; set; }
    
    [Required]
    [Length(1, 200)]
    public string Category { get; set; }
    
    [Required]
    [Length(1, 200)]
    public string Area { get; set; }
    
}