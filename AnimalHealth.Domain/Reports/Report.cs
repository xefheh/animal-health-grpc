using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class Report
    {
        public int Id { get; set; }

        public IReportState CurrentState { get; set; }

        public User Creator { get; set; }

        DateTime createDate;
        public DateTime CreateDate 
        { 
            get => createDate.ToUniversalTime();
            set => createDate = value; 
        }

        public DateTime ChangeTime
        {
            get => CurrentState.Date;
        }

        public User Changer
        {
            get => CurrentState.Changer;
        }

        public string Type { get; set; }

        public List<ReportValue> Values { get; set; } = new List<ReportValue>();

        public Report(string type)
        {
            CreateDate = DateTime.Now;
            Type = type;
        }

        public void GetReport<T>(ICollection<T> records, Func<T, (string, string)> func)
        {
            var createdState = new CreatedState(CreateDate, Creator);
            CurrentState = createdState;
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