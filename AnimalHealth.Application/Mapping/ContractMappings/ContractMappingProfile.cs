using AnimalHealth.Application.Models;
using AutoMapper;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.Identity;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.ContractMappings;

public class ContractMappingProfile : Profile
{
    public ContractMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
    }
     
    private void CreateMappingToModelFromEntity()
    {
        CreateMap<Contract, ContractModel>()
            .ForMember(model => model.Id, opt => opt.MapFrom(contract => contract.Id))
            .ForMember(model => model.Number, opt => opt.MapFrom(contract => contract.Number))
            .ForMember(model => model.ConclusionDate, opt => opt.MapFrom(contract => contract.ConclusionDate.ToTimestamp()))
            .ForMember(model => model.EndDate, opt => opt.MapFrom(contract => contract.EndDate.ToTimestamp()))
            .ForMember(model => model.Executor, opt => opt.MapFrom(contract => contract.Executor))
            .ForMember(model => model.Customer, opt => opt.MapFrom(contract => contract.Customer));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<ContractModel, Contract>()
            .ForMember(model => model.Id, opt => opt.MapFrom(model => model.Id))
            .ForMember(model => model.Number, opt => opt.MapFrom(model => model.Number))
            .ForMember(model => model.ConclusionDate, opt => opt.MapFrom(model => model.ConclusionDate.ToDateTime()))
            .ForMember(model => model.EndDate, opt => opt.MapFrom(model => model.EndDate.ToDateTime()))
            .ForMember(model => model.Executor, opt => opt.MapFrom(model => model.Executor))
            .ForMember(model => model.Customer, opt => opt.MapFrom(model => model.Customer));
    }
}