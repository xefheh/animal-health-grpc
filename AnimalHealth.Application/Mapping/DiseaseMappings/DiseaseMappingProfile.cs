using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AutoMapper;

namespace AnimalHealth.Application.Mapping.DiseaseMappings;

public class DiseaseMappingProfile : Profile
{
    public DiseaseMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
    }
    
    private void CreateMappingToModelFromEntity()
    {
        CreateMap<Disease, DiseaseModel>()
            .ForMember(model => model.Id, opt => opt.MapFrom(disease => disease.Number))
            .ForMember(model => model.Name, opt => opt.MapFrom(disease => disease.Name));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<DiseaseModel, Disease>()
            .ForMember(disease => disease.Number, opt => opt.MapFrom(model => model.Id))
            .ForMember(disease => disease.Name, opt => opt.MapFrom(model => model.Name));
    }
}