using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Interfaces.Identity;
using AnimalHealth.Application.Models;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class AuthService : AuthProto.AuthProtoBase
{
    private readonly IAuthService _service;
    private readonly ILogger<AuthService> _logger;
    
    public AuthService(IAuthService service, ILogger<AuthService> logger) => (_service, _logger) = (service, logger);

    public override async Task<RoleModel> Authorize(UserLoginModel request, ServerCallContext context)
    {
        try
        {
            _logger.LogInformation("[AUTH SERVICE] Trying login with data: {Request}", request);
            var roleModel = await _service.AuthAsync(request, context.CancellationToken);
            _logger.LogInformation("[AUTH SERVICE] Successfully. User {User} logged", roleModel.User);
            return roleModel;
        }
        catch (Exception e)
        {
            var exception = e switch
            {
                NotFoundException => new RpcException(new Status(StatusCode.NotFound,
                    $"User with login: {request.Login} not exist")),
                LoginException => new RpcException(new Status(StatusCode.PermissionDenied, "Password incorrect")),
                _ => e
            };
            _logger.LogWarning("[AUTH SERVICE] Error occured: {Error}", exception);
            throw exception;
        }
    }
}