using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.InspectionMappings;

public class InspectionMappingProfile : Profile
{
    public InspectionMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
        CreateMappingToEntityFromAddModel();
    }

    private void CreateMappingToModelFromEntity()
    {
        CreateMap<Inspection, InspectionModel>()
            .ForMember(model => model.Id, opt => opt.MapFrom(inspection => inspection.Id))
            .ForMember(model => model.Date, opt => opt.MapFrom(inspection => inspection.Date.ToTimestamp()))
            .ForMember(model => model.FeatureBehaviour, opt => opt.MapFrom(inspection => inspection.FeatureBehaviour))
            .ForMember(model => model.IsNeedOperations, opt => opt.MapFrom(inspection => inspection.IsNeedOperations))
            .ForMember(model => model.Contract, opt => opt.MapFrom(inspection => inspection.Contract))
            .ForMember(model => model.Disease, opt => opt.MapFrom(inspection => inspection.Disease))
            .ForMember(model => model.User, opt => opt.MapFrom(inspection => inspection.User))
            .ForMember(model => model.Temperature, opt => opt.MapFrom(inspection => inspection.Temperature))
            .ForMember(model => model.AnimalCondition, opt => opt.MapFrom(inspection => inspection.AnimalCondition))
            .ForMember(model => model.SkinCover, opt => opt.MapFrom(inspection => inspection.SkinCover))
            .ForMember(model => model.Injures, opt => opt.MapFrom(inspection => inspection.Injures))
            .ForMember(model => model.Treatment, opt => opt.MapFrom(inspection => inspection.Treatment))
            .ForMember(model => model.FurCondition, opt => opt.MapFrom(inspection => inspection.FurCondition))
            .ForMember(model => model.Manipulations, opt => opt.MapFrom(inspection => inspection.Manipulations))
            .ForMember(model => model.Animal, opt => opt.MapFrom(inspection => inspection.InspectedAnimal));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<InspectionModel, Inspection>()
            .ForMember(model => model.Id, opt => opt.MapFrom(model => model.Id))
            .ForMember(model => model.Date, opt => opt.MapFrom(model => model.Date.ToDateTime()))
            .ForMember(model => model.FeatureBehaviour, opt => opt.MapFrom(model => model.FeatureBehaviour))
            .ForMember(model => model.IsNeedOperations, opt => opt.MapFrom(model => model.IsNeedOperations))
            .ForMember(model => model.Contract, opt => opt.MapFrom(model => model.Contract))
            .ForMember(model => model.Disease, opt => opt.MapFrom(model => model.Disease))
            .ForMember(model => model.User, opt => opt.MapFrom(model => model.User))
            .ForMember(model => model.Temperature, opt => opt.MapFrom(model => model.Temperature))
            .ForMember(model => model.AnimalCondition, opt => opt.MapFrom(model => model.AnimalCondition))
            .ForMember(model => model.SkinCover, opt => opt.MapFrom(model => model.SkinCover))
            .ForMember(model => model.Injures, opt => opt.MapFrom(model => model.Injures))
            .ForMember(model => model.Treatment, opt => opt.MapFrom(model => model.Treatment))
            .ForMember(model => model.FurCondition, opt => opt.MapFrom(model => model.FurCondition))
            .ForMember(model => model.Manipulations, opt => opt.MapFrom(model => model.Manipulations))
            .ForMember(model => model.InspectedAnimal, opt => opt.MapFrom(model => model.Animal));
    }
    
    private void CreateMappingToEntityFromAddModel()
    {
        CreateMap<InspectionAddModel, Inspection>()
            .ForMember(model => model.Date, opt => opt.MapFrom(model => model.Date.ToDateTime()))
            .ForMember(model => model.FeatureBehaviour, opt => opt.MapFrom(model => model.FeatureBehaviour))
            .ForMember(model => model.IsNeedOperations, opt => opt.MapFrom(model => model.IsNeedOperations))
            .ForMember(model => model.Contract, opt => opt.MapFrom(model => model.Contract))
            .ForMember(model => model.Disease, opt => opt.MapFrom(model => model.Disease))
            .ForMember(model => model.User, opt => opt.MapFrom(model => model.User))
            .ForMember(model => model.Temperature, opt => opt.MapFrom(model => model.Temperature))
            .ForMember(model => model.AnimalCondition, opt => opt.MapFrom(model => model.AnimalCondition))
            .ForMember(model => model.SkinCover, opt => opt.MapFrom(model => model.SkinCover))
            .ForMember(model => model.Injures, opt => opt.MapFrom(model => model.Injures))
            .ForMember(model => model.Treatment, opt => opt.MapFrom(model => model.Treatment))
            .ForMember(model => model.FurCondition, opt => opt.MapFrom(model => model.FurCondition))
            .ForMember(model => model.Manipulations, opt => opt.MapFrom(model => model.Manipulations))
            .ForMember(model => model.InspectedAnimal, opt => opt.MapFrom(model => model.Animal));
    }
}