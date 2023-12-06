using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Extensions.OrganizationExt;
using AnimalHealth.Application.Extensions.VaccinationExt;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries.Organizations;

public class OrganizationRegistry : IOrganizationRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IMapper _mapper;

    public OrganizationRegistry(AnimalHealthContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);
    
    public async Task<OrganizationModel> GetOrganizationAsync(OrganizationLookup lookup, CancellationToken cancellationToken)
    {
        var organizationTin = lookup.Tin;
        var organization = await
            _context.Organizations.LoadIncludes()
                .FirstOrDefaultAsync(organization => organization.Tin == organizationTin, cancellationToken);
        if (organization == default(Organization)) throw new NotFoundException(typeof(Organization), organizationTin);
        return _mapper.Map<OrganizationModel>(organization);
    }

    public async Task<OrganizationModelList> GetOrganizationsAsync(CancellationToken cancellationToken)
    {
        var organizations = await _context.Organizations.LoadIncludes().ToListAsync(cancellationToken);
        var organizationModels = organizations.Select(organization => _mapper.Map<OrganizationModel>(organization));
        var organizationModelList = new OrganizationModelList();
        organizationModelList.Organizations.AddRange(organizationModels);
        return organizationModelList;
    }

    public async Task<OrganizationLookup> AddOrganizationAsync(OrganizationAddModel addedOrganization, CancellationToken cancellationToken)
    {
        var organization = _mapper.Map<Organization>(addedOrganization);
        await _context.Organizations.AddAsync(organization, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return new OrganizationLookup { Tin = organization.Tin };
    }

    public async Task<DbSaveCondition> UpdateOrganizationAsync(OrganizationModel updatedOrganization, CancellationToken cancellationToken)
    {
        var updatedDomainOrganization = _mapper.Map<Organization>(updatedOrganization);
        var organization = await
            _context.Organizations.LoadIncludes()
                .FirstOrDefaultAsync(organization => organization.Tin == updatedOrganization.Tin, cancellationToken);
        if (organization == default(Organization)) throw new NotFoundException(typeof(Organization), updatedOrganization.Tin);
        organization.UpdateFields(updatedDomainOrganization);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        return new DbSaveCondition { Code = saveCode };
    }

    public async Task<DbSaveCondition> DeleteOrganizationAsync(OrganizationLookup lookup, CancellationToken cancellationToken)
    {
        var organizationTin = lookup.Tin;
        var organizationMock = new Organization { Tin = organizationTin };
        _context.Organizations.Attach(organizationMock);
        _context.Organizations.Remove(organizationMock);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        return new DbSaveCondition { Code = saveCode };
    }
}