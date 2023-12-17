using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class ApprovedState : IReportState
    {
        DateTime date;
        public DateTime Date
        {
            get => date.ToUniversalTime();
            set => date = value;
        }

        public User Changer { get; set; }

        public User SecondApprover { get; set; }

        public string Name { get => "Одобрен"; }

        public ApprovedState(DateTime date, User changer, User secondApprover) =>
            (Date, Changer, SecondApprover) = (date, changer, secondApprover);

        public ApprovedState() { }

        public void Approve(Report report, DateTime date, User changer, User secondApprover)
        {
            return;
        }

        public void Cancel(Report report, DateTime date, User changer) =>
            report.State = new CreatedState(date, changer);

        public void Send(Report report, DateTime date, User changer, User receiver) =>
            report.State = new SentState(date, changer, receiver);
    }
}
