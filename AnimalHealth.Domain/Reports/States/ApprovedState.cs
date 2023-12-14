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

        public User Creator { get; set; }  

        public string Name { get => "Одобрен"; }

        public ApprovedState(DateTime date, User user) =>
            (Date, Changer) = (date, user);

        public ApprovedState() { }

        public void Handle(Report report, IReportState state)
        {
            throw new NotImplementedException();
        }
    }
}
