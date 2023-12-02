using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Identity;
using AutoMapper;

namespace AnimalHealth.Application.Mapping.UserMappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMappingToModelFromEntity();
        CreateMappingToEntityFromModel();
    }
     
    private void CreateMappingToModelFromEntity()
    {
        CreateMap<User, UserModel>()
            .ForMember(model => model.Id, opt => opt.MapFrom(user => user.Id))
            .ForMember(model => model.Name, opt => opt.MapFrom(user => user.Name))
            .ForMember(model => model.Login, opt => opt.MapFrom(user => user.Login))
            .ForMember(model => model.Password, opt => opt.MapFrom(user => user.Password));
    }

    private void CreateMappingToEntityFromModel()
    {
        CreateMap<UserModel, User>()
            .ForMember(user => user.Id, opt => opt.MapFrom(model => model.Id))
            .ForMember(user => user.Name, opt => opt.MapFrom(model => model.Name))
            .ForMember(user => user.Login, opt => opt.MapFrom(model => model.Login))
            .ForMember(user => user.Password, opt => opt.MapFrom(model => model.Password));
    }
}