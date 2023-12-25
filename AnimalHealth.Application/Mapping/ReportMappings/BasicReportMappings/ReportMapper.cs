using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Domain.Reports;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.ReportMappings.BasicReportMappings
{
    public class ReportMapper : IEntityMapper<Report, ReportModel>
    {
        private readonly IEntityMapper<ReportValue, ReportValueModel> _mapper;
        private readonly IEntityMapper<User, UserModel> _userMapper;

        public ReportMapper(IEntityMapper<ReportValue, ReportValueModel> mapper,
            IEntityMapper<User, UserModel> userMapper)
        {
            _mapper = mapper;
            _userMapper = userMapper;
        }

        public Report Map(ReportModel model)
        {
            throw new NotImplementedException();
        }

        public ReportModel Map(Report entity)
        {
            var rm = new ReportModel
            {
                Id = entity.Id,
                ChangeDate = entity.ChangeDate.ToTimestamp(),
                AdditionalChanger = _userMapper.Map(entity.AdditionalChanger),
                AdditionalChangerName = entity.AdditionalChangerName,
                Changer = _userMapper.Map(entity.Changer),
                ChangeDateName = entity.ChangeDateName,
                ChangerName = entity.ChangerName,
                Type = entity.Type,
                StateName = entity.StateName,
            };
            rm.Values.AddRange(entity.Values.Select(x => _mapper.Map(x)));
            return rm;
        }
    }
}
