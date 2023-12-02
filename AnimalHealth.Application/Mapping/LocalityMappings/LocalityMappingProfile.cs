using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AutoMapper;

namespace AnimalHealth.Application.Mapping.LocalityMappings;

public class LocalityMappingProfile : Profile
{
    public LocalityMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
    }
    
    private void CreateMappingToModelFromEntity()
    {
        CreateMap<Locality, LocalityModel>()
            .ForMember(model => model.Id, opt => opt.MapFrom(locality => locality.Id))
            .ForMember(model => model.Name, opt => opt.MapFrom(locality => locality.Name));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<LocalityModel, Locality>()
            .ForMember(locality => locality.Id, opt => opt.MapFrom(model => model.Id))
            .ForMember(locality => locality.Name, opt => opt.MapFrom(model => model.Name));
    }
}