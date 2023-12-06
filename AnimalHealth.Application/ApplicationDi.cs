using System.Reflection;
using AnimalHealth.Application.Interfaces.OtherSource;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.OtherViews;
using AnimalHealth.Application.Registries.Contracts;
using AnimalHealth.Application.Registries.Inspections;
using AnimalHealth.Application.Registries.Organizations;
using AnimalHealth.Application.Registries.Vaccinations;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalHealth.Application;

public static class ApplicationDi
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<IInspectionRegistry, InspectionRegistry>();
        services.AddTransient<IVaccinationRegistry, VaccinationRegistry>();
        services.AddTransient<IContractRegistry, ContractRegistry>();
        services.AddTransient<IOrganizationRegistry, OrganizationRegistry>();
        services.AddTransient<IOtherSource, OtherSource>();
        services.AddAutoMapper(profile => 
            profile.AddProfiles(GetAllProfiles(Assembly.GetExecutingAssembly())));
    }

    private static IEnumerable<Profile> GetAllProfiles(Assembly assembly)
    {
        var profiles = assembly.GetTypes().Where(type => type.BaseType == typeof(Profile));
        return profiles.Select(profile => Activator.CreateInstance(profile) as Profile);
    }
}