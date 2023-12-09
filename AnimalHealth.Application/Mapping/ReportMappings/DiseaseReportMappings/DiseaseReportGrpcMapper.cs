using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Reports.LocalityDiseaseReport;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using AnimalHealth.Domain.BasicReportEntities;
using static System.Enum;

namespace AnimalHealth.Application.Mapping.ReportMappings.DiseaseReportMappings
{
    public class DiseaseReportGrpcMapper : IEntityMapper<DiseaseReport, ReportModel>
    {
        private readonly IEntityMapper<DiseaseReportValue, ReportValueModel> _mapper;

        public DiseaseReportGrpcMapper(IEntityMapper<DiseaseReportValue, ReportValueModel> mapper)
        {
            _mapper = mapper;
        }

        public DiseaseReport Map(ReportModel model) => new()
        {
            Id = model.Id,
            CreateDate = model.CreateDate.ToDateTime(),
            State = (ReportState)Parse(typeof(ReportState), model.State),   
            User = model.UserCreator,
            Values = model.Values.Select(x => _mapper.Map(x)).ToList(),
        };

        public ReportModel Map(DiseaseReport entity)
        {
            var rm = new ReportModel
            {
                Id = entity.Id,
                CreateDate = entity.CreateDate.ToTimestamp(),
                State = entity.State.ToString(),
                UserCreator = entity.User,
                Type = entity.Type,
            };
            rm.Values.AddRange(entity.Values.Select(x => _mapper.Map(x)));
            return rm;
        }
    }
}
