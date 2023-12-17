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

        public User SecondApprover { get; set; }   

        public string Name { get => "Одобрен"; }

        public ApprovedState(DateTime date, User changer, User secondApprover) =>
            (Date, SecondApprover, Changer) = (date, secondApprover, changer);

        public ApprovedState() { }

        public void Handle(Report report, User user, DateTime date)
        {
            report.SentState = new SentState(date, user,  );
        }

        public void Cancel(Report report, User user, DateTime time)
        {
            if (report.ApprovedState is not null && report.SentState is null)
                throw new IncorrectChangeReportStateException("You cannot cancel this report!");
            report.ApprovedState = null;
        }
    }
}
