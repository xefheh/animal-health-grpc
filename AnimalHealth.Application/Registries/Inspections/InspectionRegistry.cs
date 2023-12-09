using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Extensions.InspectionExt;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Reports.LocalityAnimalTypeReport;
using AnimalHealth.Application.Reports.LocalityDiseaseReport;
using AnimalHealth.Domain.BasicReportEntities;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries.Inspections;

public class InspectionRegistry : IInspectionRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IEntityMapper<Inspection, InspectionAddModel, InspectionModel> _mapper;
    private readonly IEntityMapper<DiseaseReport, ReportModel> _diseaseReportGrpcMapper;
    private readonly IEntityMapper<AnimalTypeReport, ReportModel> _animalTypeReportGrpcMapper;
    private readonly IMapper<DiseaseReport, Report> _diseaseReportEFMapper;
    private readonly IMapper<AnimalTypeReport, Report> _animalTypeReportEFMapper;

    public InspectionRegistry(AnimalHealthContext context, IEntityMapper<Inspection, InspectionAddModel, InspectionModel> mapper,
        IEntityMapper<DiseaseReport, ReportModel> diseaseReportGrpcMapper,
        IEntityMapper<AnimalTypeReport, ReportModel> animalTypeReportGrpcMapper, 
        IMapper<DiseaseReport, Report> diseaseReportEFMapper, 
        IMapper<AnimalTypeReport, Report> animalTypeReportEFMapper)
    {
        _context = context;
        _mapper = mapper;
        _diseaseReportGrpcMapper = diseaseReportGrpcMapper;
        _animalTypeReportGrpcMapper = animalTypeReportGrpcMapper;
        _diseaseReportEFMapper = diseaseReportEFMapper;
        _animalTypeReportEFMapper = animalTypeReportEFMapper;
    }

    public async Task<InspectionModel> GetInspectionAsync(InspectionLookup lookup, CancellationToken cancellationToken)
    {
        var inspectionId = lookup.Id;
        var inspection = await _context.Inspections
            .LoadIncludes().FirstOrDefaultAsync(inspection => inspection.Id == inspectionId, cancellationToken);
        if (inspection == default(Inspection)) throw new NotFoundException(typeof(Inspection), inspectionId);
        return _mapper.Map(inspection);
    }

    public async Task<InspectionModelList> GetInspectionsAsync(CancellationToken cancellationToken)
    {
        var inspections = await _context.Inspections
            .LoadIncludes().ToListAsync(cancellationToken);
        var inspectionModels = inspections.Select(inspection => _mapper.Map(inspection));
        var inspectionModelList = new InspectionModelList();
        inspectionModelList.Inspections.AddRange(inspectionModels);
        return inspectionModelList;
    }

    public async Task<InspectionLookup> AddInspectionAsync(InspectionAddModel addedInspection, CancellationToken cancellationToken)
    {
        var inspection = _mapper.Map(addedInspection);
        _context.AttachNestedEntities(inspection);
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
        var inspections = await _context.Inspections
            .LoadIncludes()
            .Where(ins => ins.Date >= dates.DateStart.ToDateTime() && ins.Date <= dates.DateEnd.ToDateTime())
            .ToListAsync(cancellationToken);

        var report = new AnimalTypeReport();
        report.GetReport(inspections);
        report.User = dates.UserCreator;
        await _context.Reports.AddAsync(_animalTypeReportEFMapper.Map(report), cancellationToken);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        return _animalTypeReportGrpcMapper.Map(report);
    }

    public async Task<ReportModel> GetDiseaseReportAsync(GetReport dates, CancellationToken cancellationToken)
    {
        var inspections = await _context.Inspections
            .LoadIncludes()
            .Where(ins => ins.Date >= dates.DateStart.ToDateTime() && ins.Date <= dates.DateEnd.ToDateTime())
            .ToListAsync(cancellationToken);

        var report = new DiseaseReport();
        report.User = dates.UserCreator;
        report.GetReport(inspections);
        await _context.Reports.AddAsync(_diseaseReportEFMapper.Map(report), cancellationToken);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        return _diseaseReportGrpcMapper.Map(report);
    }
}