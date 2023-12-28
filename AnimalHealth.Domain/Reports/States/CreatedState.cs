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

        public string DateName => "Дата создания";

        public User Changer { get; set; }

        public string ChangerName => "Создатель";

        public string Name => "Черновик";

        public User AdditionalChanger { get; set; }

        public string AdditionalChangerName => "Руководитель";

        public CreatedState(DateTime date, User user, User user2) =>
            (Date, Changer, AdditionalChanger) = (date, user, user2);

        public CreatedState() { }

        public void Handle(Report report, DateTime date, List<User> users)
        {
            var state = new ApprovedState
            {
                Date = date,
                Changer = users[0],
                AdditionalChanger = users[1],
            };
            report.CurrentState = state;
        }
    }
}