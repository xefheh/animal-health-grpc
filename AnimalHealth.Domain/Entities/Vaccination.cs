using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Entities;

public class Vaccination
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Animal Animal { get; set; } = null!;
    public User User { get; set; } = null!;
    public Vaccine Vaccine { get; set; } = null!;
    public Contract Contract { get; set; } = null!;
}