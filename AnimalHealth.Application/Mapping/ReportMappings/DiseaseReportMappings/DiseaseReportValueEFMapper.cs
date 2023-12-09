using AnimalHealth.Application.Interfaces;
using AnimalHealth.Domain.BasicReportEntities;
using AnimalHealth.Application.Reports.LocalityDiseaseReport;

namespace AnimalHealth.Application.Mapping.ReportMappings.DiseaseReportMappings
{
    public class DiseaseReportValueEFMapper : IMapper<DiseaseReportValue, ReportValue>
    {
        public DiseaseReportValue Map(ReportValue model) => new()
        {
            Disease = model.SecondFeature,
            Locality = model.FirstFeature,
            Count = model.Count,
            Id = model.Id
        };

        public ReportValue Map(DiseaseReportValue entity) => new()
        {
            Id = entity.Id,
            FirstFeature = entity.Locality,
            SecondFeature = entity.Disease,
            Count = entity.Count,
        };
    }
}
