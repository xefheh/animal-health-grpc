using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class VaccinationService : VaccinationProto.VaccinationProtoBase
{
    private readonly IVaccinationRegistry _registry;
    private readonly ILogger<VaccinationService> _logger;

    public VaccinationService(IVaccinationRegistry registry, ILogger<VaccinationService> logger) =>
        (_registry, _logger) = (registry, logger);

    public override async Task<VaccinationModel> GetVaccination(VaccinationLookup request, ServerCallContext context)
    {
        _logger.LogInformation("[VACCINATION SERVICE] Invoked to get the vaccination with Id: {Id}", request.Id);
        try
        {
            var vaccination = await _registry.GetVaccinationAsync(request, context.CancellationToken);
            _logger.LogInformation("[VACCINATION SERVICE] Successfully. The resulting gRPC model of the vaccination: {@Model}", vaccination);
            return vaccination;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[VACCINATION SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Vaccination not exist"));
        }
    }

    public override async Task<VaccinationModelList> GetVaccinations(Empty request, ServerCallContext context)
    {
        _logger.LogInformation($"[VACCINATION SERVICE] Invoked to get the vaccination list");
        var vaccinations = await _registry.GetVaccinationsAsync(context.CancellationToken);
        _logger.LogInformation("[VACCINATION SERVICE] Successfully. Vaccination List: {@List}; Count: {Count}",
            vaccinations.Vaccinations, vaccinations.Vaccinations.Count());
        return vaccinations;
    }

    public override async Task<VaccinationLookup> AddVaccination(VaccinationAddModel request, ServerCallContext context)
    {
        _logger.LogInformation("[VACCINATION SERVICE] Invoked to add the vaccination from model: {@Model}", request);
        try
        {
            var vaccinationLookup = await _registry.AddVaccinationAsync(request, context.CancellationToken);
            _logger.LogInformation("[VACCINATION SERVICE] Successfully. Id of added vaccination: {ID}", vaccinationLookup.Id);
            return vaccinationLookup;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[VACCINATION SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Nested objects not exist"));
        }
    }

    public override async Task<DbSaveCondition> UpdateVaccination(VaccinationModel request, ServerCallContext context)
    {
        _logger.LogInformation("[VACCINATION SERVICE] Invoked to update the vaccination with id {ID}", request.Id);
        try
        {
            var dbCondition = await _registry.UpdateVaccinationAsync(request, context.CancellationToken);
            _logger.LogInformation("[VACCINATION SERVICE] Successfully. Model of updated vaccination: {Model}", request);
            return dbCondition;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[VACCINATION SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Nested objects not exist"));
        }
    }

    public override async Task<DbSaveCondition> DeleteVaccination(VaccinationLookup request, ServerCallContext context)
    {
        _logger.LogInformation("[VACCINATION SERVICE] Invoked to delete the vaccination with id: {ID}", request.Id);
        var dbSaveCondition = await _registry.DeleteVaccinationAsync(request, context.CancellationToken);
        _logger.LogInformation("[VACCINATION SERVICE] Successfully. Vaccination is deleted");
        return dbSaveCondition;
    }

    public override async Task<ReportModel> GetVaccinationReport(GetReport request, ServerCallContext context)
    {
        _logger.LogInformation("[VACCINATION SERVICE] User: {User} invoke to get VACCINATION report. Period: ({Start} / {End})",
            request.UserCreator,
            request.DateStart, request.DateEnd);
        var reportModel = await _registry.GetVaccinationReportAsync(request, context.CancellationToken);
        _logger.LogInformation("[VACCINATION SERVICE] Successfully. VACCINATION Report created by {User}. Report values: {ReportValue} (Count: {Count})",
            request.UserCreator, reportModel.Values, reportModel.Values.Count);
        return reportModel;
    }
}