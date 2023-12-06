using AnimalHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AnimalHealth.Application.Extensions.IncludeLoadingExtensions;

public static class OrganizationIncludeExtension
{
    public static IIncludableQueryable<Organization, Locality?> LoadIncludes(
        this IQueryable<Organization> organizations) => organizations
        .Include(organization => organization.Locality);
}