using AnimalHealth.Application.Models;
using AutoMapper;
using AnimalHealth.Domain.Entities;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.ContractMappings;

public class ContractMappingProfile : Profile
{
    public ContractMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
        CreateMappingToEntityFromAddModel();
    }
     
    private void CreateMappingToModelFromEntity()
    {
        CreateMap<Contract, ContractModel>()
            .ForMember(model => model.Id, opt => opt.MapFrom(contract => contract.Id))
            .ForMember(model => model.Number, opt => opt.MapFrom(contract => contract.Number))
            .ForMember(model => model.ConclusionDate,
                opt => opt.MapFrom(contract => contract.ConclusionDate.ToTimestamp()))
            .ForMember(model => model.EndDate, opt => opt.MapFrom(contract => contract.EndDate.ToTimestamp()));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<ContractModel, Contract>()
            .ForMember(model => model.Id, opt => opt.MapFrom(model => model.Id))
            .ForMember(model => model.Number, opt => opt.MapFrom(model => model.Number))
            .ForMember(model => model.ConclusionDate, opt => opt.MapFrom(model => model.ConclusionDate.ToDateTime()))
            .ForMember(model => model.EndDate, opt => opt.MapFrom(model => model.EndDate.ToDateTime()));
    }
    
    private void CreateMappingToEntityFromAddModel()
    {
        CreateMap<ContractAddModel, Contract>()
            .ForMember(model => model.Number, opt => opt.MapFrom(model => model.Number))
            .ForMember(model => model.ConclusionDate, opt => opt.MapFrom(model => model.ConclusionDate.ToDateTime()))
            .ForMember(model => model.EndDate, opt => opt.MapFrom(model => model.EndDate.ToDateTime()));
    }
}