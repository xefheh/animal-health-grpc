using AnimalHealth.Application.Interfaces;
using AnimalHealth.Domain.BasicReportEntities;
using AnimalHealth.Application.Reports.LocalityDiseaseReport;

namespace AnimalHealth.Application.Mapping.ReportMappings.DiseaseReportMappings
{
    public class DiseaseReportEFMapper : IMapper<DiseaseReport, Report>
    {
        readonly IMapper<DiseaseReportValue, ReportValue> _mapper;

        public DiseaseReportEFMapper(IMapper<DiseaseReportValue, ReportValue> mapper)
        {
            _mapper = mapper;
        }

        public DiseaseReport Map(Report model) => new()
        {
            Id = model.Id,
            CreateDate = model.CreateDate,
            State =  model.State,
            User = model.User,
            Values = model.Values.Select(x => _mapper.Map(x)).ToList(),
        };

        public Report Map(DiseaseReport entity) => new()
        {
            Id = entity.Id,
            CreateDate = entity.CreateDate,
            State = entity.State,
            User = entity.User,
            Values = entity.Values.Select(x => _mapper.Map(x)).ToList(),
            Type = entity.Type,
        };
    }
}
