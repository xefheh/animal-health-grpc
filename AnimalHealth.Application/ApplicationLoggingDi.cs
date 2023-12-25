using AnimalHealth.Application.Factories;
using AnimalHealth.Application.Identity.Interfaces;
using AnimalHealth.Application.Identity.Logging;
using AnimalHealth.Application.OtherSources.Interfaces;
using AnimalHealth.Application.OtherSources.Logging;
using AnimalHealth.Application.Registries.Interfaces;
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