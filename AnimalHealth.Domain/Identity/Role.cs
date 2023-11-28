using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Domain.Identity;

public class Role
{
    public int Id { get; set; }
    public Organization Organization { get; set; } = null!;
    public User User { get; set; } = null!;
}