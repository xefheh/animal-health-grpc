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

        public void Cancel(Report report, DateTime date, User changer) =>
            throw new IncorrectChangeReportStateException("You cannot cancel not approved report!");

        public void Approve(Report report, DateTime date, User changer, User secondApprover) =>
            report.State = new ApprovedState(date, changer, secondApprover);

        public void Send(Report report, DateTime date, User changer, User receiver) =>
            throw new IncorrectChangeReportStateException("You cannot send not approved report!");

    }
}