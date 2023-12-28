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

        public User AdditionalChanger { get; set; }

        public string DateName => "Дата отправки";

        public string ChangerName => "Отправитель";

        public string AdditionalChangerName => "Получатель";

        public SentState() { }

        public void Handle(Report report, DateTime date, List<User> users)
        {
            return;
        }
    }
}
