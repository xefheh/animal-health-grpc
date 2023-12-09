using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Reports.LocalityVaccinationReport;
using AnimalHealth.Domain.BasicReportEntities;

namespace AnimalHealth.Application.Mapping.ReportMappings.VaccinationReportMappings
{
    public class VaccinationReportValueEFMapper : IMapper<VaccinationReportValue, ReportValue>
    {
        public VaccinationReportValue Map(ReportValue model) => new()
        {
            Id = model.Id,
            Count = model.Count,
            Locality = model.FirstFeature,
            Vaccine = model.SecondFeature,
        };

        public ReportValue Map(VaccinationReportValue entity) => new()
        {
            Id = entity.Id,
            Count = entity.Count,
            FirstFeature = entity.Locality,
            SecondFeature = entity.Vaccine,
        };
    }
}
