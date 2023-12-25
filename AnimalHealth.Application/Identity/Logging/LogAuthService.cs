using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Identity.Interfaces;
using AnimalHealth.Application.Models;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace AnimalHealth.Application.Identity.Logging;

public class LogAuthService : IAuthService
{
    private readonly IAuthService _service;
    private readonly ILogger<IAuthService> _logger;

    public LogAuthService(IAuthService service, ILogger<IAuthService> logger) =>
        (_service, _logger) = (service, logger);
    
    public async Task<RoleModel> AuthAsync(UserLoginModel loginModel, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("[AUTH SERVICE] Trying login with data: {Request}", loginModel);
            var roleModel = await _service.AuthAsync(loginModel, cancellationToken);
            _logger.LogInformation("[AUTH SERVICE] Successfully. User {User} logged", roleModel.User);
            return roleModel;
        }
        catch (Exception e)
        {
            var exception = e switch
            {
                NotFoundException => new RpcException(new Status(StatusCode.NotFound,
                    $"User with login: {loginModel.Login} not exist")),
                LoginException => new RpcException(new Status(StatusCode.PermissionDenied, "Password incorrect")),
                _ => e
            };
            _logger.LogWarning("[AUTH SERVICE] Error occured: {Error}", exception);
            throw exception;
        }
    }
}