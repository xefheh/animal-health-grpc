using System.Reflection;
using AnimalHealth.Domain.Reports;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Persistence;

public class AnimalHealthContext : DbContext
{
    public AnimalHealthContext(DbContextOptions opt) : base(opt) { } 
    
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
    public DbSet<Report> Reports { get; set; }
    public DbSet<ReportValue> ReportValues { get; set; }
    public DbSet<IReportState> States { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    
    /// <summary>
    /// Отслеживать изменение вложенных сущностей.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="entity">Сущность, у которой необходимо отслеживать вложенные сущности.</param>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    /// <exception cref="ArgumentNullException">Сущности в БД нет.</exception>
    public void AttachNestedEntities<TEntity>(TEntity? entity)
    {
        var navigations = Model.FindEntityType(typeof(TEntity))?.GetNavigations();
        var getters = (navigations ?? throw new ArgumentNullException(nameof(navigations))).Select(navigation => navigation.GetGetter());
        var nestedEntities = getters.Select(getter => getter.GetClrValue(entity ?? throw new ArgumentNullException(nameof(entity))));
        foreach (var nestedEntity in nestedEntities)
            Attach(nestedEntity ?? throw new ArgumentNullException(nameof(nestedEntity)));
    }
}