using AnimalHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AnimalHealth.Application.Extensions.IncludeLoadingExtensions;

public static class VaccinationIncludeExtension
{
    public static IIncludableQueryable<Vaccination, Locality?> LoadIncludes(this IQueryable<Vaccination> inspections) => inspections
        .Include(inspection => inspection.Vaccine)
        .Include(inspection => inspection.User)
        .Include(inspection => inspection.Animal)
        .Include(inspection => inspection.Contract)
        .ThenInclude(contract => contract.Customer)
        .ThenInclude(organization => organization.Locality)
        .Include(inspection => inspection.Contract)
        .ThenInclude(contract => contract.Executor)
        .ThenInclude(organization => organization.Locality);
}