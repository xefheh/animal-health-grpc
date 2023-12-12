using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class Report
    {
        public int Id { get; set; }
        public IReportState State { get; set; }
        public User User { get; set; }
        DateTime createDate;
        public DateTime CreateDate 
        { 
            get => createDate.ToUniversalTime();
            set => createDate = value; 
        }
        public string Type { get; set; }
        public List<ReportValue> Values { get; set; }

        public Report()
        {
            Values = new List<ReportValue>();
            State = new CreatedState(CreateDate, User);
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

        public void Approve(DateTime date, User user) =>
            State.Approve(this, date, user);

        public void Send(DateTime date, User user) =>
            State.Send(this, date, user);

        public void Cancel(DateTime date, User user) =>
            State.Cancel(this, date, user);
    }
}