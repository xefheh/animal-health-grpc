using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Identity;
using AutoMapper;

namespace AnimalHealth.Application.Mapping.RoleMappings;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
    }
     
    private void CreateMappingToModelFromEntity()
    {
        CreateMap<Role, RoleModel>()
            .ForMember(model => model.Id, opt => opt.MapFrom(role => role.Id))
            .ForMember(model => model.User, opt => opt.MapFrom(role => role.User))
            .ForMember(model => model.Organization, opt => opt.MapFrom(role => role.Organization));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<RoleModel, Role>()
            .ForMember(role => role.Id, opt => opt.MapFrom(model => model.Id))
            .ForMember(role => role.User, opt => opt.MapFrom(model => model.User))
            .ForMember(role => role.Organization, opt => opt.MapFrom(model => model.Organization));
    }
}