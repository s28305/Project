using System.ComponentModel.DataAnnotations;

namespace Project.SoftwareSystems.Models;

public class SoftwareSystem
{
    public int Id { get; set; }
    
    [Required]
    [Length(1, 50)]
    public required string Name { get; set; }
    
    [Required]
    [Length(1, 150)]
    public required string Description { get; set; }
    
    [Required]
    [Length(1, 50)]
    public required string Version { get; set; }
    
    [Required]
    [Length(1, 50)]
    public required string Category { get; set; } 
    public bool HasSubscription { get; set; }
    public double UpfrontCost { get; set; }
}
