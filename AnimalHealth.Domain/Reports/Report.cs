using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class Report
    {
        public int Id { get; set; }

        public IReportState CurrentState { get; set; }

        public DateTime ChangeDate => CurrentState.Date;
        public string ChangeDateName => CurrentState.DateName;

        public User Changer => CurrentState.Changer;
        public string ChangerName => CurrentState.ChangerName;

        public User AdditionalChanger => CurrentState.AdditionalChanger;
        public string AdditionalChangerName => CurrentState.AdditionalChangerName;

        public string StateName => CurrentState.Name;

        public string Type { get; set; }

        public List<ReportValue> Values { get; set; } = new List<ReportValue>();

        public Report(string type, User user1, User user2)
        {
            var createdState = new CreatedState(DateTime.Now, user1, user2);
            CurrentState = createdState;
            Type = type;
        }

        public void GetReport<T>(ICollection<T> records, Func<T, (string, string)> func)
        {
            foreach (var record in records)
            {
                var locDis = func(record);
                var reportValue = new ReportValue(locDis.Item1, locDis.Item2);
                if (!Values.Contains(reportValue))
                    Values.Add(reportValue);
                else findReportValue(reportValue).Count += 1;
            }
        }

        ReportValue findReportValue(ReportValue value) =>
            Values.Find((pr => pr.Equals(value)));

        public void GoNextState(DateTime date, List<User> users)
        {
            CurrentState.Handle(this, date, users);
        }
    }
}