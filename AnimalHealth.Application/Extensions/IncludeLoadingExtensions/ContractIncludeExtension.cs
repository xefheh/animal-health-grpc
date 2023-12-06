using AnimalHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AnimalHealth.Application.Extensions.IncludeLoadingExtensions;

public static class ContractIncludeExtension
{
    public static IIncludableQueryable<Contract, Locality?> LoadIncludes(this IQueryable<Contract> contracts) =>
        contracts
            .Include(contract => contract.Customer)
            .ThenInclude(organization => organization.Locality)
            .Include(contract => contract.Executor)
            .ThenInclude(organization => organization.Locality);
}