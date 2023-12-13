using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Reports.LocalityAnimalTypeReport;
using AnimalHealth.Domain.BasicReportEntities;
using static System.Enum;

namespace AnimalHealth.Application.Mapping.ReportMappings.AnimalTypeReportMappings
{
    public class AnimalTypeReportEFMapper : IMapper<AnimalTypeReport, Report>
    {
        private readonly IMapper<AnimalTypeReportValue, ReportValue> _mapper;

        public AnimalTypeReportEFMapper(IMapper<AnimalTypeReportValue, ReportValue> mapper)
        {
            this._mapper = mapper;
        }

        public AnimalTypeReport Map(Report model) => new()
        {
            Id = model.Id,
            CreateDate = model.CreateDate,
            State = model.State,
            User = model.User,
            Values = model.Values.Select(x => _mapper.Map(x)).ToList(),
        };

        public Report Map(AnimalTypeReport entity) => new()
        {
            Id = entity.Id,
            CreateDate = entity.CreateDate,
            State = entity.State,
            User = entity.User,
            Type = entity.Type,
            Values = entity.Values.Select(x => _mapper.Map(x)).ToList(),
        };
    }
}
