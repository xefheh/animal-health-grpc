using AnimalHealth.Domain.Abstract;

namespace AnimalHealth.Domain.Entities;

public class Animal : UpdatableEntity<Animal>
{
    public int RegNumber { get; set; }
    public string? Name { get; set; }
    public string? OwnerFeatures { get; set; }
    public DateTime BirthDate { get; set; }
    public string? BehaviourFeatures { get; set; }
    public string? Sex { get; set; }
    public string? Type { get; set; }
    public int ChipNumber { get; set; }
}