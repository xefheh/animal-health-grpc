using AnimalHealth.Application.Identity;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.Identity;
using AnimalHealth.Application.Interfaces.OtherSource;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Mapping.EntityMappings;
using AnimalHealth.Application.Mapping.ReportMappings.BasicReportMappings;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.OtherViews;
using AnimalHealth.Application.Registries.Contracts;
using AnimalHealth.Application.Registries.Inspections;
using AnimalHealth.Application.Registries.Organizations;
using AnimalHealth.Application.Registries.Reports;
using AnimalHealth.Application.Registries.Vaccinations;
using AnimalHealth.Domain.Reports;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.Identity;
using Microsoft.Extensions.DependencyInjection;
using AnimalHealth.Application.Mapping.ReportMappings;

namespace AnimalHealth.Application;

public static class ApplicationDi
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<IInspectionRegistry, InspectionRegistry>();
        services.AddTransient<IVaccinationRegistry, VaccinationRegistry>();
        services.AddTransient<IContractRegistry, ContractRegistry>();
        services.AddTransient<IOrganizationRegistry, OrganizationRegistry>();
        services.AddTransient<IReportRegistry, ReportRegistryList>();
        services.AddTransient<IOtherSource, OtherSource>();
        services.AddTransient<IEntityMapper<Locality, LocalityModel>, LocalityMapper>();
        services.AddTransient<IEntityMapper<User, UserModel>, UserMapper>();
        services.AddTransient<IEntityMapper<Disease, DiseaseModel>, DiseaseMapper>();
        services.AddTransient<IEntityMapper<Animal, AnimalModel>, AnimalMapper>();
        services.AddTransient<IEntityMapper<Vaccine, VaccineModel>, VaccineMapper>();
        services.AddTransient<IEntityMapper<Organization, OrganizationAddModel, OrganizationModel>, OrganizationMapper>();
        services.AddTransient<IEntityMapper<Contract, ContractAddModel, ContractModel>, ContractMapper>();
        services.AddTransient<IEntityMapper<Inspection, InspectionAddModel, InspectionModel>, InspectionMapper>();
        services.AddTransient<IEntityMapper<Vaccination, VaccinationAddModel, VaccinationModel>, VaccinationMapper>();
        services.AddTransient<IEntityMapper<Report, ReportModel>, ReportMapper>();
        services.AddTransient<IEntityMapper<ReportValue, ReportValueModel>, ReportValueMapper>();
        services.AddTransient<IAuthService, AuthService>();
    }
}