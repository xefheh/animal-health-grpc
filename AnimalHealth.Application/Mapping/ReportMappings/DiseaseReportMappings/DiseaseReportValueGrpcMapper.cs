using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Reports.LocalityDiseaseReport;
using AnimalHealth.Application.Models;

namespace AnimalHealth.Application.Mapping.ReportMappings.DiseaseReportMappings
{
    public class DiseaseReportValueGrpcMapper : IEntityMapper<DiseaseReportValue, ReportValueModel>
    {
        public DiseaseReportValue Map(ReportValueModel model) => new()
        {
            Id = model.Id,
            Locality = model.FirstFeature,
            Disease = model.SecondFeature,
            Count = model.Count
        };

        public ReportValueModel Map(DiseaseReportValue entity) => new()
        {
            Id = entity.Id,
            FirstFeature = entity.Locality,
            SecondFeature = entity.Disease,
            Count = entity.Count
        };
    }
}
