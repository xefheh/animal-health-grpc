﻿using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class VaccinationService : VaccinationProto.VaccinationProtoBase
{
    private readonly IVaccinationRegistry _registry;

    public VaccinationService(IVaccinationRegistry registry) => _registry = registry;

    public override async Task<VaccinationModel> GetVaccination(VaccinationLookup request, ServerCallContext context)
    {
        try
        {
            return await _registry.GetVaccinationAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<VaccinationModelList> GetVaccinations(Empty request, ServerCallContext context) =>
        await _registry.GetVaccinationsAsync(context.CancellationToken);

    public override async Task<VaccinationLookup> AddVaccination(VaccinationAddModel request, ServerCallContext context)
    {
        try
        {
            return await _registry.AddVaccinationAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<DbSaveCondition> UpdateVaccination(VaccinationModel request, ServerCallContext context)
    {
        try
        {
            return await _registry.UpdateVaccinationAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<DbSaveCondition>
        DeleteVaccination(VaccinationLookup request, ServerCallContext context) =>
        await _registry.DeleteVaccinationAsync(request, context.CancellationToken);

    public override async Task<ReportModel> GetVaccinationReport(GetReport request, ServerCallContext context) =>
        await _registry.GetVaccinationReportAsync(request, context.CancellationToken);
}