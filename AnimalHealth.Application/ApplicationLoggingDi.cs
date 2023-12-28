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
        services.Decorate<IAuthService, LogAuthService>();
        services.Decorate<IContractRegistry, LogContractRegistry>();
        services.Decorate<IInspectionRegistry, LogInspectionRegistry>();
        services.Decorate<IOrganizationRegistry, LogOrganizationRegistry>();
        services.Decorate<IOtherSource, LogOtherSource>();
        services.Decorate<IReportRegistry, LogReportRegistry>();
        services.Decorate<IVaccinationRegistry, LogVaccinationRegistry>();
    }
}