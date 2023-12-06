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
            .ForMember(model => model.Price, opt => opt.MapFrom(pair => pair.Price));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<PricePairModel, PricePair>()
            .ForMember(pair => pair.Id, opt => opt.MapFrom(model => model.Id))
            .ForMember(pair => pair.Price, opt => opt.MapFrom(model => model.Price));
    }
}