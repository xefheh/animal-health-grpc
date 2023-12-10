using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Reports.LocalityVaccinationReport;

namespace AnimalHealth.Application.Mapping.ReportMappings.VaccinationReportMappings
{
    public class VaccinationReportValueGrpcMapper : IEntityMapper<VaccinationReportValue, ReportValueModel>
    {
        public VaccinationReportValue Map(ReportValueModel model) => new()
        {
            Id = model.Id,
            Count = model.Count,
            Locality = model.FirstFeature,
            Vaccine = model.SecondFeature,
        };

        public ReportValueModel Map(VaccinationReportValue entity) => new()
        {
            Id = entity.Id,
            Count = entity.Count,
            FirstFeature = entity.Locality,
            SecondFeature = entity.Vaccine
        };
    }
}
