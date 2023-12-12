using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Reports;

namespace AnimalHealth.Application.Mapping.ReportMappings.BasicReportMappings
{
    public class ReportValueMapper : IEntityMapper<ReportValue, ReportValueModel>
    {
        public ReportValue Map(ReportValueModel model) => new()
        {
            Id = model.Id,
            Count = model.Count,
            FirstFeature = model.FirstFeature,
            SecondFeature = model.SecondFeature,
        };

        public ReportValueModel Map(ReportValue entity) => new()
        {
            Id = entity.Id,
            Count = entity.Count,
            FirstFeature = entity.FirstFeature,
            SecondFeature = entity.SecondFeature,
        };
    }
}
