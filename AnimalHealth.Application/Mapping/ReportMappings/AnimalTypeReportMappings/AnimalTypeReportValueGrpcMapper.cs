using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Reports.LocalityAnimalTypeReport;

namespace AnimalHealth.Application.Mapping.ReportMappings.AnimalTypeReportMappings
{
    public class AnimalTypeReportValueGrpcMapper : IEntityMapper<AnimalTypeReportValue, ReportValueModel>
    {
        public AnimalTypeReportValue Map(ReportValueModel model) => new()
        {
            Id = model.Id,
            Count = model.Count,
            Locality = model.FirstFeature,
            AnimalType = model.SecondFeature,
        };

        public ReportValueModel Map(AnimalTypeReportValue entity) => new()
        {
            Id = entity.Id,
            Count = entity.Count,
            FirstFeature = entity.Locality,
            SecondFeature = entity.AnimalType,
        };
    }
}
