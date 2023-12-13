using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Application.Mapping.EntityMappings;

public class UserMapper : IEntityMapper<User, UserModel>
{
    public User Map(UserModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
    };

    public UserModel Map(User entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name
    };
}