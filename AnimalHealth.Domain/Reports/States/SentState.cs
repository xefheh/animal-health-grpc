using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class SentState : IReportState
    {
        DateTime date;
        public DateTime Date
        {
            get => date.ToUniversalTime();
            set => date = value;
        }

        public User Creator { get; set; }

        public User Approver { get; set; }

        public User Changer { get; set; }

        public User Receiver { get; set; } 

        public string Name => "Отправлен";

        public SentState(DateTime date, User user) =>
            (Date, Changer) = (date, user);

        public SentState() { }

        public void Handle(Report report, IReportState state)
        {
            throw new NotImplementedException();
        }
    }
}
