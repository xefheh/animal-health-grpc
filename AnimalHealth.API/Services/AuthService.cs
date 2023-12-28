using AnimalHealth.Application.Identity.Interfaces;
using AnimalHealth.Application.Models;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class AuthService : AuthProto.AuthProtoBase
{
    private readonly IAuthService _service;

    public AuthService(IAuthService service) => _service = service;

    public override async Task<RoleModel> Authorize(UserLoginModel request, ServerCallContext context) =>
        await _service.AuthAsync(request, context.CancellationToken);
}