using AnimalHealth.Domain.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalHealth.Domain.Reports
{
    [NotMapped]
    public class Report
    {
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public IReportState State { get; set; }
        [NotMapped]
        public User Creator { get; set; }
        DateTime createDate;
        [NotMapped]
        public DateTime CreateDate 
        { 
            get => createDate.ToUniversalTime();
            set => createDate = value; 
        }
        [NotMapped]
        public virtual string Type { get; set; }
        [NotMapped]
        public List<ReportValue> Values { get; set; }

        public Report()
        {
            Values = new List<ReportValue>();
            CreateDate = DateTime.Now;
        }

        public void GetReport<T>(ICollection<T> records, Func<T, (string, string)> func)
        {
            State = new CreatedState(CreateDate, Creator);
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