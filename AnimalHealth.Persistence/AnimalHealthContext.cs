using System.Reflection;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Persistence;

public class AnimalHealthContext : DbContext
{
    public AnimalHealthContext(DbContextOptions opt) : base(opt)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<PricePair> PricePairs { get; set; }
    public DbSet<Locality> Localities { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Vaccine> Vaccines { get; set; }
    public DbSet<Vaccination> Vaccinations { get; set; }
    public DbSet<Disease> Diseases { get; set; }
    public DbSet<Inspection> Inspections { get; set; }
    public DbSet<Contract> Contracts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}