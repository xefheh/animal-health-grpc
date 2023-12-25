using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Identity.Interfaces;
using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Identity;

public class AuthService : IAuthService
{
    private readonly AnimalHealthContext _context;
    private readonly IEntityMapper<User, UserModel> _userMapper;

    public AuthService(AnimalHealthContext context, IEntityMapper<User, UserModel> userMapper) => 
        (_context, _userMapper) = (context, userMapper);

    public async Task<RoleModel> AuthAsync(UserLoginModel loginModel, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Login == loginModel.Login, cancellationToken);
        if (user == default(User)) throw new NotFoundException(typeof(User), user.Login);
        if (user.Password != loginModel.Password) throw new LoginException();
        var role = await _context.Roles.FirstOrDefaultAsync(role => role.User == user, cancellationToken);
        if (role == default(Role)) throw new LoginException();
        return new RoleModel { Id = role.Id, User = _userMapper.Map(user) };
    }
}