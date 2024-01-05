using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Domain.Reports;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries;

public class InspectionRegistry : IInspectionRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IReportRegistry _reportRegistry;
    private readonly IEntityMapper<Inspection, InspectionAddModel, InspectionModel> _mapper;
    private readonly IEntityMapper<Report, ReportModel> _reportGrpcMapper;
    private readonly IEntityMapper<User, UserModel> _userGrpcMapper;

    public InspectionRegistry(AnimalHealthContext context,
        IReportRegistry reportRegistry,
        IEntityMapper<Inspection, InspectionAddModel, InspectionModel> mapper, 
        IEntityMapper<Report, ReportModel> reportGrpcMapper,
        IEntityMapper<User, UserModel> userGrpcMapper)
    {
        _context = context;
        _reportRegistry = reportRegistry;
        _mapper = mapper;
        _reportGrpcMapper = reportGrpcMapper;
        _userGrpcMapper = userGrpcMapper;
    }

    public async Task<InspectionModel> GetInspectionAsync(InspectionLookup lookup, CancellationToken cancellationToken)
    {
        var inspections = _context.Inspections.Local.ToList();
        if (!inspections.Any()) inspections = await _context.Inspections.LoadIncludes().ToListAsync(cancellationToken);
        var inspectionId = lookup.Id;
        var resultInspection = inspections.FirstOrDefault(inspection => inspection.Id == inspectionId);
        if (resultInspection == default(Inspection)) throw new NotFoundException(typeof(Inspection), inspectionId);
        return _mapper.Map(resultInspection);
    }

    public async Task<InspectionModelList> GetInspectionsAsync(CancellationToken cancellationToken)
    {
        var inspections = _context.Inspections.Local.ToList();
        if (!inspections.Any()) inspections = await _context.Inspections.LoadIncludes().ToListAsync(cancellationToken);
        var inspectionModels = inspections.Select(inspection => _mapper.Map(inspection));
        var inspectionModelList = new InspectionModelList();
        inspectionModelList.Inspections.AddRange(inspectionModels);
        return inspectionModelList;
    }

    public async Task<InspectionLookup> AddInspectionAsync(InspectionAddModel addedInspection, CancellationToken cancellationToken)
    {
        var inspection = _mapper.Map(addedInspection);
        await _context.Inspections.AddAsync(inspection, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return new InspectionLookup { Id = inspection.Id };
    }

    public async Task<DbSaveCondition> UpdateInspectionAsync(InspectionModel updatedInspection, CancellationToken cancellationToken)
    {
        var updatedDomainInspection = _mapper.Map(updatedInspection);
        var inspection =
            await _context.Inspections.FindAsync(new object[] { updatedDomainInspection.Id }, cancellationToken);
        if (inspection == default(Inspection)) throw new NotFoundException(typeof(Vaccination), updatedInspection.Id);
        inspection.UpdateFields(updatedDomainInspection);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        return new DbSaveCondition { Code = saveCode };
    }

    public async Task<DbSaveCondition> DeleteInspectionAsync(InspectionLookup lookup, CancellationToken cancellationToken)
    {
        var inspectionId = lookup.Id;
        var inspectionMock = new Inspection { Id = inspectionId };
        _context.Inspections.Attach(inspectionMock);
        _context.Inspections.Remove(inspectionMock);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        return new DbSaveCondition { Code = saveCode };
    }

    public async Task<ReportModel> GetAnimalTypeReportAsync(GetReport dates, CancellationToken cancellationToken)
    {
        var allInspections = _context.Inspections.Local.ToList();
        if (!allInspections.Any()) allInspections = await _context.Inspections.LoadIncludes().ToListAsync(cancellationToken);

        var inspections = allInspections
            .Where(ins => ins.Date >= dates.DateStart.ToDateTime() && ins.Date <= dates.DateEnd.ToDateTime())
            .ToList();

        var creator = _userGrpcMapper.Map(dates.UserCreator);
        var creator2 = _userGrpcMapper.Map(dates.SecondUser);
        var report = new Report("По нас.пункту и типам животных", creator, creator2);

        report.GetReport(inspections, (inspection) => inspection.GetLocalityAnimalType());

        await _reportRegistry.AddReportAsync(report, cancellationToken);

        return _reportGrpcMapper.Map(report);
    }

    public async Task<ReportModel> GetDiseaseReportAsync(GetReport dates, CancellationToken cancellationToken)
    {
        var allInspections = _context.Inspections.Local.ToList();
        if (!allInspections.Any()) allInspections = await _context.Inspections.LoadIncludes().ToListAsync(cancellationToken);

        var inspections = allInspections
            .Where(ins => ins.Date >= dates.DateStart.ToDateTime() && ins.Date <= dates.DateEnd.ToDateTime())
            .ToList();

        var creator = _userGrpcMapper.Map(dates.UserCreator);
        var creator2 = _userGrpcMapper.Map(dates.SecondUser);
        var report = new Report("По нас.пункту и болезням", creator, creator2);
        report.GetReport(inspections, (inspection) => inspection.GetLocalityDisease()); 

        await _reportRegistry.AddReportAsync(report, cancellationToken);

        return _reportGrpcMapper.Map(report);
    }
}