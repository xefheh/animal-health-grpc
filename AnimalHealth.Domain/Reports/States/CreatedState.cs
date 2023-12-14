using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class CreatedState : IReportState
    {
        DateTime date;
        public DateTime Date
        {
            get => date.ToUniversalTime();
            set => date = value;
        }

        public User Changer { get; set; }

        public string Name => "Черновик";

        public CreatedState(DateTime date, User user) =>
            (Date, Changer) = (date, user);

        public CreatedState() { }

        public void Approve(Report report, DateTime date, User user) =>
            report.State = new ApprovedState(date, user);

        public void Cancel(Report report, DateTime date, User user) =>
            throw new IncorrectChangeReportStateException("You cannot cancel not approved report!");

        public void Send(Report report, DateTime date, User user) =>
            throw new IncorrectChangeReportStateException("You cannot send not approved report!");
    }
}
