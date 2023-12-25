using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.EntityMappings;

public class AnimalMapper : IEntityMapper<Animal, AnimalModel>
{
    public Animal Map(AnimalModel model) => new()
    {
        RegNumber = model.RegNumber,
        Name = model.Name,
        BirthDate = model.BirthDate.ToDateTime(),
        BehaviourFeatures = model.BehaviourFeatures,
        ChipNumber = model.ChipNumber,
        OwnerFeatures = model.OwnerFeatures,
        Sex = model.Sex,
        Type = model.Sex
    };

    public AnimalModel Map(Animal entity) => new()
    {
        RegNumber = entity.RegNumber,
        Name = entity.Name,
        BirthDate = entity.BirthDate.ToTimestamp(),
        BehaviourFeatures = entity.BehaviourFeatures,
        ChipNumber = entity.ChipNumber,
        OwnerFeatures = entity.OwnerFeatures,
        Sex = entity.Sex,
        Type = entity.Sex
    };
}