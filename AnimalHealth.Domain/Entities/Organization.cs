using AnimalHealth.Domain.Abstract;

namespace AnimalHealth.Domain.Entities;

public class Organization : UpdatableEntity<Organization>
{
    public string Tin { get; set; } = null!;
    public string Trc { get; set; } = null!;
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Feature { get; set; }
    public Locality? Locality { get; set; }
}