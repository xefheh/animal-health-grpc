using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Application.Mapping.EntityMappings;

public class LocalityMapper : IEntityMapper<Locality, LocalityModel>
{
    public Locality Map(LocalityModel model) => new()
    {
        Id = model.Id,
        Name = model.Name
    };

    public LocalityModel Map(Locality entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name
    };
}