using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Application.Mapping.EntityMappings;

public class DiseaseMapper : IEntityMapper<Disease, DiseaseModel>
{
    public Disease Map(DiseaseModel model) => new()
    {
        Number = model.Id,
        Name = model.Name
    };

    public DiseaseModel Map(Disease entity) => new()
    {
        Id = entity.Number,
        Name = entity.Name
    };
}