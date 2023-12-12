﻿using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Extensions.InspectionExt;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Reports;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;
using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Application.Registries.Inspections;

public class InspectionRegistry : IInspectionRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IReportRegistry _reportRegistry;
    private readonly IEntityMapper<Inspection, InspectionAddModel, InspectionModel> _mapper;
    private readonly IEntityMapper<DiseaseReport, ReportModel> _diseaseReportGrpcMapper;
    private readonly IEntityMapper<AnimalTypeReport, ReportModel> _animalTypeReportGrpcMapper;
    private readonly IEntityMapper<User, UserModel> _userGrpcMapper;
    private readonly IMapper<DiseaseReport, Report> _diseaseReportEFMapper;
    private readonly IMapper<AnimalTypeReport, Report> _animalTypeReportEFMapper;

    public InspectionRegistry(AnimalHealthContext context, IReportRegistry reportRegistry, 
        IEntityMapper<Inspection, InspectionAddModel, InspectionModel> mapper,
        IEntityMapper<DiseaseReport, ReportModel> diseaseReportGrpcMapper, 
        IEntityMapper<AnimalTypeReport, ReportModel> animalTypeReportGrpcMapper, 
        IEntityMapper<User, UserModel> userGrpcMapper, 
        IMapper<DiseaseReport, Report> diseaseReportEFMapper,
        IMapper<AnimalTypeReport, Report> animalTypeReportEFMapper)
    {
        _context = context;
        this._reportRegistry = reportRegistry;
        _mapper = mapper;
        _diseaseReportGrpcMapper = diseaseReportGrpcMapper;
        _animalTypeReportGrpcMapper = animalTypeReportGrpcMapper;
        _userGrpcMapper = userGrpcMapper;
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
        report.GetReport(inspections, (inspection) => inspection.GetLocalityAnimalType());
        report.User = _userGrpcMapper.Map(dates.UserCreator);

        await _reportRegistry.AddReportAsync(report, cancellationToken);

        var efreport = await _context.Reports.Where(x => x.CreateDate == report.CreateDate).FirstAsync(cancellationToken);
        report.Id = efreport.Id;

        return _animalTypeReportGrpcMapper.Map(report);
    }

    public async Task<ReportModel> GetDiseaseReportAsync(GetReport dates, CancellationToken cancellationToken)
    {
        var inspections = await _context.Inspections
            .LoadIncludes()
            .Where(ins => ins.Date >= dates.DateStart.ToDateTime() && ins.Date <= dates.DateEnd.ToDateTime())
            .ToListAsync(cancellationToken);

        var report = new DiseaseReport();
        report.GetReport(inspections, (inspection) => inspection.GetLocalityDisease());
        report.User = _userGrpcMapper.Map(dates.UserCreator);

        await _reportRegistry.AddReportAsync(report, cancellationToken);

        var efreport = await _context.Reports.Where(x => x.CreateDate == report.CreateDate).FirstAsync(cancellationToken);
        report.Id = efreport.Id;

        return _diseaseReportGrpcMapper.Map(report);
    }
}