namespace AnimalHealth.Domain.Entities;

public class Contract
{
    public int Id { get; set; }
    public int Number { get; set; }
    public DateTime ConclusionDate { get; set; }
    public DateTime EndDate { get; set; }
    public Organization Executor { get; set; } = null!;
    public virtual Organization Customer { get; set; } = null!;

    public string GetExecutorLocality()
        => Executor.Locality.Name;
}