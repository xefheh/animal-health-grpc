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
        public string DateName => "Дата утверждения";

        public User Changer { get; set; }
        public string ChangerName => "Утвердитель";

        public string Name { get => "Одобрен"; }
        public User AdditionalChanger { get; set; }
        public string AdditionalChangerName => "Второй утвердитель";

        public ApprovedState() { }

        public void Handle(Report report, DateTime date, List<User> users)
        {
            report.CurrentState = new SentState
            {
                Date = date,
                Changer = users[0],
                AdditionalChanger = users[1],
            };
        }
    }
}