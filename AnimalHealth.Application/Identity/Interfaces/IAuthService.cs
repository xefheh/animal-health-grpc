using AnimalHealth.Application.Models;

namespace AnimalHealth.Application.Identity.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Авторизация.
    /// </summary>
    /// <param name="loginModel">gRPC модель авторизации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>gRPC модель роли.</returns>
    Task<RoleModel> AuthAsync(UserLoginModel loginModel, CancellationToken cancellationToken);
}