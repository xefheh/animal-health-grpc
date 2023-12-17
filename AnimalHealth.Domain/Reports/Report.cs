using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class Report
    {
        public int Id { get; set; }

        CreatedState createdState;
        public CreatedState CreatedState 
        {
            get => createdState;
            set
            {
                CurrentState = value;
                createdState = value;
            } 
        }  
        ApprovedState approvedState;
        public ApprovedState ApprovedState
        {
            get => approvedState;
            set
            {
                if (value == null) CurrentState = CreatedState;
                else
                {
                    CurrentState = value;
                    approvedState = value;
                }
            }
        }
        SentState sentState;
        public SentState SentState 
        {
            get => sentState;
            set
            {
                CurrentState = value; 
                sentState = value;
            }
        }    

        public IReportState CurrentState { get; set; }

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
            CreatedState = new CreatedState(CreateDate, Creator);
            CurrentState = CreatedState;
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

        public void GoNextState(DateTime date, User user, User secondApprover = null, User Receiver = null)
        {
            CurrentState.Handle(this, user, date);
        }

        public void Cancel(DateTime date, User user) 
        {
            if (CurrentState is ApprovedState approvedState)
            approvedState.Cancel(this, user, date);
        }
    }
}