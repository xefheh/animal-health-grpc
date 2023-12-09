using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Reports.LocalityAnimalTypeReport;
using AnimalHealth.Domain.BasicReportEntities;

namespace AnimalHealth.Application.Mapping.ReportMappings.AnimalTypeReportMappings
{
    public class AnimalTypeReportValueEFMapper : IMapper<AnimalTypeReportValue, ReportValue>
    {
        public AnimalTypeReportValue Map(ReportValue model) => new()
        {
            Id = model.Id,
            Count = model.Count,
            Locality = model.FirstFeature,
            AnimalType = model.SecondFeature,
        };

        public ReportValue Map(AnimalTypeReportValue entity) => new()
        {
            Id = entity.Id,
            Count = entity.Count,
            FirstFeature = entity.Locality,
            SecondFeature = entity.AnimalType,
        };
    }
}