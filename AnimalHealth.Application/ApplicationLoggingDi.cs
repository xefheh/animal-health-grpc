using AnimalHealth.Application.Factories;
using AnimalHealth.Application.Interfaces.Identity;
using AnimalHealth.Application.Interfaces.OtherSource;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Registries.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalHealth.Application;

public static class ApplicationLoggingDi
{
    public static void AddLogApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<LogRegistryFactory<IAuthService, LogAuthService>>();
        services.AddTransient<LogRegistryFactory<IInspectionRegistry, LogInspectionRegistry>>();
        services.AddTransient<LogRegistryFactory<IReportRegistry, LogReportRegistry>>();
        services.AddTransient<LogRegistryFactory<IOtherSource, LogOtherSource>>();
        services.AddTransient<LogRegistryFactory<IOrganizationRegistry, LogOrganizationRegistry>>();
        services.AddTransient<LogRegistryFactory<IContractRegistry, LogContractRegistry>>();
        services.AddTransient<LogRegistryFactory<IVaccinationRegistry, LogVaccinationRegistry>>();
    }
}