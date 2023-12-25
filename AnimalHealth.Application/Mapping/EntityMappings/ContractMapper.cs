using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.EntityMappings;

public class ContractMapper : IEntityMapper<Contract, ContractAddModel, ContractModel>
{
    private readonly AnimalHealthContext _context;
    private readonly IEntityMapper<Organization, OrganizationAddModel, OrganizationModel> _orgMapper;

    public ContractMapper(AnimalHealthContext context, IEntityMapper<Organization, OrganizationAddModel, OrganizationModel> orgMapper) => 
        (_context, _orgMapper) = (context, orgMapper);
    
    public Contract Map(ContractAddModel model) => new()
    {
        Number = model.Number,
        ConclusionDate = model.ConclusionDate.ToDateTime(),
        EndDate = model.EndDate.ToDateTime(),
        Customer = _context.Organizations.Find(model.Customer.Tin) ??
                   throw new NotFoundException(typeof(Organization), model.Customer.Tin),
        Executor = _context.Organizations.Find(model.Executor.Tin) ??
                   throw new NotFoundException(typeof(Organization), model.Executor.Tin)
    };

    public Contract Map(ContractModel model) => new()
    {
        Id = model.Id,
        Number = model.Number,
        ConclusionDate = model.ConclusionDate.ToDateTime(),
        EndDate = model.EndDate.ToDateTime(),
        Customer = _context.Organizations.Find(model.Customer.Tin) ??
                   throw new NotFoundException(typeof(Organization), model.Customer.Tin),
        Executor = _context.Organizations.Find(model.Executor.Tin) ??
                   throw new NotFoundException(typeof(Organization), model.Executor.Tin)
    };

    public ContractModel Map(Contract entity) => new()
    {
        Id = entity.Id,
        Number = entity.Number,
        ConclusionDate = entity.ConclusionDate.ToTimestamp(),
        EndDate = entity.EndDate.ToTimestamp(),
        Customer = _orgMapper.Map(entity.Customer),
        Executor = _orgMapper.Map(entity.Executor)
    };
}