using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.VaccinationMappings;

public class VaccinationMappingProfile : Profile
{
    public VaccinationMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
        CreateMappingToEntityFromAddModel();
    }

    private void CreateMappingToModelFromEntity()
    {
        CreateMap<Vaccination, VaccinationModel>()
            .ForMember(model => model.Id, opt => opt.MapFrom(vaccination => vaccination.Id))
            .ForMember(model => model.Date, opt => opt.MapFrom(vaccination => vaccination.Date.ToTimestamp()))
            .ForMember(model => model.ExpirationDate, opt => opt.MapFrom(vaccination => vaccination.ExpirationDate.ToTimestamp()))
            .ForMember(model => model.Animal, opt => opt.MapFrom(vaccination => vaccination.Animal))
            .ForMember(model => model.Contract, opt => opt.MapFrom(vaccination => vaccination.Contract))
            .ForMember(model => model.Vaccine, opt => opt.MapFrom(vaccination => vaccination.Vaccine))
            .ForMember(model => model.User, opt => opt.MapFrom(vaccination => vaccination.User));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<VaccinationModel, Vaccination>()
            .ForMember(vaccination => vaccination.Id, opt => opt.MapFrom(model => model.Id))
            .ForMember(vaccination => vaccination.Date, opt => opt.MapFrom(model => model.Date.ToDateTime()))
            .ForMember(vaccination => vaccination.ExpirationDate, opt => opt.MapFrom(model => model.ExpirationDate.ToDateTime()))
            .ForMember(vaccination => vaccination.Animal, opt => opt.MapFrom(model => model.Animal))
            .ForMember(vaccination => vaccination.Contract, opt => opt.MapFrom(model => model.Contract))
            .ForMember(vaccination => vaccination.Vaccine, opt => opt.MapFrom(model => model.Vaccine))
            .ForMember(vaccination => vaccination.User, opt => opt.MapFrom(model => model.User));
    }
    
    private void CreateMappingToEntityFromAddModel()
    {
        CreateMap<VaccinationAddModel, Vaccination>()
            .ForMember(vaccination => vaccination.Date, opt => opt.MapFrom(model => model.Date.ToDateTime()))
            .ForMember(vaccination => vaccination.ExpirationDate, opt => opt.MapFrom(model => model.ExpirationDate.ToDateTime()))
            .ForMember(vaccination => vaccination.Animal, opt => opt.MapFrom(model => model.Animal))
            .ForMember(vaccination => vaccination.Contract, opt => opt.MapFrom(model => model.Contract))
            .ForMember(vaccination => vaccination.Vaccine, opt => opt.MapFrom(model => model.Vaccine))
            .ForMember(vaccination => vaccination.User, opt => opt.MapFrom(model => model.User));
    }
}