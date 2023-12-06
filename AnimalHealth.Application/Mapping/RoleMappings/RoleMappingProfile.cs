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
            .ForMember(model => model.Id, opt => opt.MapFrom(role => role.Id));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<RoleModel, Role>()
            .ForMember(role => role.Id, opt => opt.MapFrom(model => model.Id));
    }
}