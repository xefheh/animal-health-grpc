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

        public User Changer { get; set; }

        public string Name => "Отправлен";

        public SentState(DateTime date, User user) =>
            (Date, Changer) = (date, user);

        public SentState() { }

        public void Approve(Report report, DateTime date, User user) =>
            throw new IncorrectChangeReportStateException("This report has been sent already!");

        public void Cancel(Report report, DateTime date, User user) =>
            throw new IncorrectChangeReportStateException("This report has been sent already!");

        public void Send(Report report, DateTime date, User user)
        {
            return;
        }
    }
}
