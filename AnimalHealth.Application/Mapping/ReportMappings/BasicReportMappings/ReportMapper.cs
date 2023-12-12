using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Domain.Reports;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.ReportMappings.BasicReportMappings
{
    public class ReportMapper : IEntityMapper<Report, ReportModel>
    {
        private readonly IEntityMapper<ReportValue, ReportValueModel> _mapper;
        private readonly IEntityMapper<IReportState, ReportStateModel> _stateMapper;
        private readonly IEntityMapper<User, UserModel> _userMapper;

        public ReportMapper(IEntityMapper<ReportValue, ReportValueModel> mapper,
            IEntityMapper<IReportState, ReportStateModel> stateMapper, 
            IEntityMapper<User, UserModel> userMapper)
        {
            _mapper = mapper;
            _stateMapper = stateMapper;
            _userMapper = userMapper;
        }

        public Report Map(ReportModel model) => new()
        {
            Id = model.Id,
            CreateDate = model.CreateDate.ToDateTime(),
            State = _stateMapper.Map(model.State),
            Type = model.Type,
            User = _userMapper.Map(model.UserCreator),
            Values = model.Values.Select(x => _mapper.Map(x)).ToList(),
        };

        public ReportModel Map(Report entity)
        {
            var rm = new ReportModel
            {
                Id = entity.Id,
                State = _stateMapper.Map(entity.State),
                CreateDate = entity.CreateDate.ToTimestamp(),
                Type = entity.Type,
                UserCreator = _userMapper.Map(entity.User),
            };
            rm.Values.AddRange(entity.Values.Select(x => _mapper.Map(x)));
            return rm;
        }
    }
}
