using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Domain.Reports;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.ReportMappings
{
    public class StateMapper : IEntityMapper<IReportState, ReportStateModel>
    {
        private readonly IEntityMapper<User, UserModel> _userMapper;

        public StateMapper(IEntityMapper<User, UserModel> userMapper)
        {
            _userMapper = userMapper;
        }

        public IReportState Map(ReportStateModel model) 
        {
            var date = model.ChangeDate.ToDateTime();
            var user = _userMapper.Map(model.Changer);
            switch (model.Name)
            {
                case "Черновик":
                    return new CreatedState(date, user);
                case "Утвержён":
                    var sa = _userMapper.Map(model.SecondApprover);
                    return new ApprovedState(date, user, sa);
                case "Отправлен":
                    var r = _userMapper.Map(model.Receiver);
                    return new SentState(date, user, r);
                default:
                    throw new Exception("This state does not exist!");
            }
        }

        public ReportStateModel Map(IReportState entity) => new()
        {
            Name = entity.Name,
            ChangeDate = entity.Date.ToTimestamp(),
            Changer = _userMapper.Map(entity.Changer),
        };
    }
}