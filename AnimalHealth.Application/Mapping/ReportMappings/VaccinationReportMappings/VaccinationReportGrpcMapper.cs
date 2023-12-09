using AnimalHealth.Domain.BasicReportEntities;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Reports.LocalityVaccinationReport;
using Google.Protobuf.WellKnownTypes;
using static System.Enum;

namespace AnimalHealth.Application.Mapping.ReportMappings.VaccinationReportMappings
{
    public class VaccinationReportGrpcMapper : IEntityMapper<VaccinationReport, ReportModel>
    {
        readonly IEntityMapper<VaccinationReportValue, ReportValueModel> _mapper;

        public VaccinationReportGrpcMapper(IEntityMapper<VaccinationReportValue, ReportValueModel> mapper)
        {
            _mapper = mapper;
        }

        public VaccinationReport Map(ReportModel model) => new()
        {
            Id = model.Id,
            CreateDate = model.CreateDate.ToDateTime(),
            User = model.UserCreator,
            Values = model.Values.Select(x => _mapper.Map(x)).ToList(),
            State = (ReportState)Parse(typeof(ReportState), model.State),
        };

        public ReportModel Map(VaccinationReport entity)
        {
            var rm = new ReportModel
            {
                Id = entity.Id,
                CreateDate = entity.CreateDate.ToTimestamp(),
                State = entity.State.ToString(),
                Type = entity.Type,
                UserCreator = entity.User,
            };
            rm.Values.AddRange(entity.Values.Select(x => _mapper.Map(x)));
            return rm;
        }
    }
}
