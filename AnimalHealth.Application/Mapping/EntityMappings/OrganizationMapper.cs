using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;

namespace AnimalHealth.Application.Mapping.EntityMappings;

public class OrganizationMapper : IEntityMapper<Organization, OrganizationAddModel, OrganizationModel>
{
    private readonly AnimalHealthContext _context;
    private readonly IEntityMapper<Locality, LocalityModel> _localityMapper;

    public OrganizationMapper(AnimalHealthContext context, IEntityMapper<Locality, LocalityModel> localityMapper) => 
        (_context, _localityMapper) = (context, localityMapper);

    public Organization Map(OrganizationAddModel model) => new()
    {
        Tin = model.Tin,
        Feature = model.Feature,
        Locality = _context.Localities.Find(model.Locality.Id) ?? throw new NotFoundException(typeof(Locality), model.Locality.Id),
        Name = model.Name,
        Trc = model.Trc,
        Type = model.Type
    };

    public Organization Map(OrganizationModel model) => new()
    {
        Tin = model.Tin,
        Feature = model.Feature,
        Locality = _context.Localities.Find(model.Locality.Id) ?? throw new NotFoundException(typeof(Locality), model.Locality.Id),
        Name = model.Name,
        Trc = model.Trc,
        Type = model.Type
    };

    public OrganizationModel Map(Organization entity) => new()
    {
        Tin = entity.Tin,
        Feature = entity.Feature,
        Locality = _localityMapper.Map(entity.Locality!),
        Name = entity.Name,
        Trc = entity.Trc,
        Type = entity.Type
    };
}