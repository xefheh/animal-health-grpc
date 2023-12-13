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

        public User Changer { get;  set; }

        public string Name { get => "Одобрен"; }

        public ApprovedState(DateTime date, User user) =>
            (Date, Changer) = (date, user);

        public ApprovedState() { }

        public void Approve(Report report, DateTime date, User user)
        {
            return;
        }

        public void Cancel(Report report, DateTime date, User user) =>
            report.State = new CreatedState(date, user);

        public void Send(Report report, DateTime date, User user) =>
            report.State = new SentState(date, user);
    }
}
