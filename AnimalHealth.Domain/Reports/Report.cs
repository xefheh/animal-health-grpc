using System.ComponentModel.DataAnnotations.Schema;
using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class Report
    {
        public int Id { get; set; }
        [NotMapped]
        public IReportState State { get; set; }

        public User Creator { get; set; }
        DateTime createDate;
        public DateTime CreateDate 
        { 
            get => createDate.ToUniversalTime();
            set => createDate = value; 
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

        public void Approve(DateTime date, User user)
        {
            var state = new ApprovedState();
            State.Handle(this, state);
        }
           
        public void Send(DateTime date, User user)
        {
            var state = new SentState();
            State.Handle(this, state);
        }

        public void Cancel(DateTime date, User user) 
        {   
            var state = new CreatedState();
            State.Handle(this, state);
        }
    }
}