using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.BasicReportEntities;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.ReportMappings.BasicReportMappings
{
    public class ReportMapper : IEntityMapper<Report, ReportModel>
    {
        private readonly IEntityMapper<ReportValue, ReportValueModel> _mapper;

        public ReportMapper(IEntityMapper<ReportValue, ReportValueModel> mapper)
        {
            _mapper = mapper;
        }

        public Report Map(ReportModel model) => new()
        {
            Id = model.Id,
            CreateDate = model.CreateDate.ToDateTime(),
            State = (ReportState)System.Enum.Parse(typeof(ReportState), model.State),
            Type = model.Type,
            User = model.UserCreator,
            Values = model.Values.Select(x => _mapper.Map(x)).ToList(),
        };

        public ReportModel Map(Report entity)
        {
            var rm = new ReportModel
            {
                Id = entity.Id,
                State = entity.State.ToString(),
                CreateDate = entity.CreateDate.ToTimestamp(),
                Type = entity.Type,
                UserCreator = entity.User,
            };
            rm.Values.AddRange(entity.Values.Select(x => _mapper.Map(x)));
            return rm;
        }
    }
}
