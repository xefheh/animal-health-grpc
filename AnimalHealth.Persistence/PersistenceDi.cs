using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalHealth.Persistence;

public static class PersistenceDi
{
    public static void AddPersistenceLayer(this ServiceCollection services, Action<DbContextOptionsBuilder> opt)
    {
        if (opt == null) throw new ArgumentNullException(nameof(opt));
        services.AddDbContext<AnimalHealthContext>(opt);
    }
}