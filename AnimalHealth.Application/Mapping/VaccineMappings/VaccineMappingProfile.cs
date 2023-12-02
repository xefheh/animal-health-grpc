using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AutoMapper;

namespace AnimalHealth.Application.Mapping.VaccineMappings;

public class VaccineMappingProfile : Profile
{
    public VaccineMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
    }
    
    private void CreateMappingToModelFromEntity()
    {
        CreateMap<Vaccine, VaccineModel>()
            .ForMember(model => model.Serial, opt => opt.MapFrom(vaccine => vaccine.Serial))
            .ForMember(model => model.Name, opt => opt.MapFrom(vaccine => vaccine.Name))
            .ForMember(model => model.Price, opt => opt.MapFrom(vaccine => vaccine.Price));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<VaccineModel, Vaccine>()
            .ForMember(vaccine => vaccine.Serial, opt => opt.MapFrom(model => model.Serial))
            .ForMember(vaccine => vaccine.Name, opt => opt.MapFrom(model => model.Name))
            .ForMember(vaccine => vaccine.Price, opt => opt.MapFrom(model => model.Price));
    }
}