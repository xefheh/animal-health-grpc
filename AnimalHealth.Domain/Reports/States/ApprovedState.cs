﻿using AnimalHealth.Domain.Identity;

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

        public User Changer { get;  set; }

        public User SecondApprover { get; set; }   

        public string Name { get => "Одобрен"; }

        public ApprovedState(DateTime date, User changer, User secondApprover) =>
            (Date, SecondApprover, Changer) = (date, secondApprover, changer);

        public ApprovedState() { }

        public void Handle(Report report, DateTime date, List<User> users)
        {
            report.CurrentState = new SentState(date, users[0], users[1]);
        }
    }
}