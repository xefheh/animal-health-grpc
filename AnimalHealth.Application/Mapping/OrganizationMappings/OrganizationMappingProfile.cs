using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AutoMapper;

namespace AnimalHealth.Application.Mapping.OrganizationMappings;

public class OrganizationMappingProfile : Profile
{
    public OrganizationMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
    }
    
    private void CreateMappingToModelFromEntity()
    {
        CreateMap<Organization, OrganizationModel>()
            .ForMember(model => model.Tin, opt => opt.MapFrom(organization => organization.Tin))
            .ForMember(model => model.Trc, opt => opt.MapFrom(organization => organization.Trc))
            .ForMember(model => model.Name, opt => opt.MapFrom(organization => organization.Name))
            .ForMember(model => model.Type, opt => opt.MapFrom(organization => organization.Type))
            .ForMember(model => model.Feature, opt => opt.MapFrom(organization => organization.Feature))
            .ForMember(model => model.Locality, opt => opt.MapFrom(organization => organization.Locality));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<OrganizationModel, Organization>()
            .ForMember(organization => organization.Tin, opt => opt.MapFrom(model => model.Tin))
            .ForMember(organization => organization.Trc, opt => opt.MapFrom(model => model.Trc))
            .ForMember(organization => organization.Name, opt => opt.MapFrom(model => model.Name))
            .ForMember(organization => organization.Type, opt => opt.MapFrom(model => model.Type))
            .ForMember(organization => organization.Feature, opt => opt.MapFrom(model => model.Feature))
            .ForMember(organization => organization.Locality, opt => opt.MapFrom(model => model.Locality));
    }
}