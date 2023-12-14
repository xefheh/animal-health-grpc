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

        public void Handle(Report report, IReportState state)
        {
            handle(report, state);
        }

        private void handle(Report report, ApprovedState state)
        {

        }

        private void handle(Report report, SentState state)
        {

        }
    }
}
