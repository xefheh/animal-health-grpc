using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AutoMapper;

namespace AnimalHealth.Application.Mapping.PairMappings;

public class PairMappingProfile : Profile
{
    public PairMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
    }
     
    private void CreateMappingToModelFromEntity()
    {
        CreateMap<PricePair, PricePairModel>()
            .ForMember(model => model.Id, opt => opt.MapFrom(pair => pair.Id))
            .ForMember(model => model.Locality, opt => opt.MapFrom(pair => pair.Locality))
            .ForMember(model => model.Contract, opt => opt.MapFrom(pair => pair.Contract))
            .ForMember(model => model.Price, opt => opt.MapFrom(pair => pair.Price));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<PricePairModel, PricePair>()
            .ForMember(pair => pair.Id, opt => opt.MapFrom(model => model.Id))
            .ForMember(pair => pair.Locality, opt => opt.MapFrom(model => model.Locality))
            .ForMember(pair => pair.Contract, opt => opt.MapFrom(model => model.Contract))
            .ForMember(pair => pair.Price, opt => opt.MapFrom(model => model.Price));
    }
}