using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Reports.LocalityAnimalTypeReport;
using AnimalHealth.Domain.BasicReportEntities;
using Google.Protobuf.WellKnownTypes;
using static System.Enum;

namespace AnimalHealth.Application.Mapping.ReportMappings.AnimalTypeReportMappings
{
    public class AnimalTypeReportGrpcMapper : IEntityMapper<AnimalTypeReport, ReportModel>
    {
        private readonly IEntityMapper<AnimalTypeReportValue, ReportValueModel> _mapper;

        public AnimalTypeReportGrpcMapper(IEntityMapper<AnimalTypeReportValue, ReportValueModel> mapper)
        {
            _mapper = mapper;
        }

        public AnimalTypeReport Map(ReportModel model) => new()
        {
            Id = model.Id,
            CreateDate = model.CreateDate.ToDateTime(),
            State = (ReportState)Parse(typeof(ReportState), model.State),
            User = model.UserCreator,
            Values = model.Values.Select(x => _mapper.Map(x)).ToList(),
        };

        public ReportModel Map(AnimalTypeReport entity) 
        {
            var rm = new ReportModel
            {
                Id = entity.Id,
                CreateDate = entity.CreateDate.ToTimestamp(),
                State = entity.State.ToString(),
                UserCreator = entity.User,
                Type = entity.Type,
            };
            rm.Values.AddRange(entity.Values.Select(x => _mapper.Map(x)).ToList());
            return rm;
        }
    }
}
