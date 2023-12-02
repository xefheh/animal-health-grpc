using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.AnimalMappings;

public class AnimalMappingProfile : Profile
{
    public AnimalMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
    }
    
        private void CreateMappingToModelFromEntity()
        {
            CreateMap<Animal, AnimalModel>()
                .ForMember(model => model.RegNumber, opt => opt.MapFrom(animal => animal.RegNumber))
                .ForMember(model => model.Name, opt => opt.MapFrom(animal => animal.Name))
                .ForMember(model => model.OwnerFeatures, opt => opt.MapFrom(animal => animal.OwnerFeatures))
                .ForMember(model => model.BirthDate, opt => opt.MapFrom(animal => animal.BirthDate.ToTimestamp()))
                .ForMember(model => model.BehaviourFeatures, opt => opt.MapFrom(animal => animal.BehaviourFeatures))
                .ForMember(model => model.Sex, opt => opt.MapFrom(animal => animal.Sex))
                .ForMember(model => model.Type, opt => opt.MapFrom(animal => animal.Type))
                .ForMember(model => model.ChipNumber, opt => opt.MapFrom(animal => animal.ChipNumber));
        }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<AnimalModel, Animal>()
            .ForMember(animal => animal.RegNumber, opt => opt.MapFrom(model => model.RegNumber))
            .ForMember(animal => animal.Name, opt => opt.MapFrom(model => model.Name))
            .ForMember(animal => animal.OwnerFeatures, opt => opt.MapFrom(model => model.OwnerFeatures))
            .ForMember(animal => animal.BirthDate, opt => opt.MapFrom(model => model.BirthDate.ToDateTime()))
            .ForMember(animal => animal.BehaviourFeatures, opt => opt.MapFrom(model => model.BehaviourFeatures))
            .ForMember(animal => animal.Sex, opt => opt.MapFrom(model => model.Sex))
            .ForMember(animal => animal.Type, opt => opt.MapFrom(model => model.Type))
            .ForMember(animal => animal.ChipNumber, opt => opt.MapFrom(model => model.ChipNumber));
    }
}