using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Entities;

public class Inspection
{
    public int Id { get; set; }
    public string? FeatureBehaviour { get; set; }
    public string? AnimalCondition { get; set; }
    public float Temperature { get; set; }
    public string? SkinCover { get; set; }
    public string? FurCondition { get; set; }
    public string? Injures { get; set; }
    public bool IsNeedOperations { get; set; }
    public string? Manipulations { get; set; }
    public string? Treatment { get; set; }
    public DateTime Date { get; set; }
    public User Doctor { get; set; } = null!;
    public Animal InspectedAnimal { get; set; } = null!;
    public Contract Contract { get; set; } = null!;
    public Disease? Disease { get; set; }
}