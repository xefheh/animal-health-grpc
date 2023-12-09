using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Reports.LocalityVaccinationReport;
using AnimalHealth.Domain.BasicReportEntities;
using static System.Enum;

namespace AnimalHealth.Application.Mapping.ReportMappings.VaccinationReportMappings
{
    public class VaccinationReportEFMapper : IMapper<VaccinationReport, Report>
    {
        private readonly IMapper<VaccinationReportValue, ReportValue> _mapper;

        public VaccinationReportEFMapper(IMapper<VaccinationReportValue, ReportValue> mapper)
        {
            _mapper = mapper;
        }

        public VaccinationReport Map(Report model) => new()
        {
            Id = model.Id,
            CreateDate = model.CreateDate,
            State = (ReportState)Enum.Parse(typeof(ReportState), model.State.ToString()),
            User = model.User,
            Values = model.Values.Select(x => _mapper.Map(x)).ToList(),
        };

        public Report Map(VaccinationReport entity)
        {
            var rm = new Report
            {
                Id = entity.Id,
                CreateDate = entity.CreateDate,
                State = entity.State,
                Type = entity.Type,
                User = entity.User,
            };
            rm.Values.AddRange(entity.Values.Select(x => _mapper.Map(x)));
            return rm;
        }
    }
}
