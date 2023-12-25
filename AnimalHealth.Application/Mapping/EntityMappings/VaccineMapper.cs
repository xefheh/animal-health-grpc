using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Application.Mapping.EntityMappings;

public class VaccineMapper : IEntityMapper<Vaccine, VaccineModel>
{
    public Vaccine Map(VaccineModel model) => new()
    {
        Serial = model.Serial,
        Name = model.Name,
        Price = model.Price
    };

    public VaccineModel Map(Vaccine entity) => new()
    {
        Serial = entity.Serial,
        Name = entity.Name,
        Price = entity.Price
    };
}