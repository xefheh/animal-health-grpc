namespace AnimalHealth.Domain.Entities;

public class PricePair
{
    public int Id { get; set; }
    public Locality Locality { get; set; } = null!;
    public Contract Contract { get; set; } = null!;
}