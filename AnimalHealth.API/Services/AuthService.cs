using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Factories;
using AnimalHealth.Application.Identity.Interfaces;
using AnimalHealth.Application.Identity.Logging;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Logging;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class AuthService : AuthProto.AuthProtoBase
{
    private readonly IAuthService _service;

    public AuthService(LogRegistryFactory<IAuthService, LogAuthService> factory,
        ILogger<AuthService> logger) => _service = factory.CreateLogRegistry();

    public override async Task<RoleModel> Authorize(UserLoginModel request, ServerCallContext context) =>
        await _service.AuthAsync(request, context.CancellationToken);
}