﻿using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Application.Reports.LocalityDiseaseReport
{
    public class DiseaseReport
    {
        public int Id { get; set; }
        public ReportState State { get; set; }
        public string User { get; set; }
        public DateTime CreateDate { get; set; }
        public string Type { get; set; }    
        public List<DiseaseReportValue> Values { get; set; } 

        public DiseaseReport()
        {
            Values = new List<DiseaseReportValue>();
            State = ReportState.Created;
        }
        
        public void GetReport(ICollection<Inspection> inspections)
        {
            foreach (var inspection in inspections)
            {
                var locDis = inspection.GetLocalityDisease();
                var reportValue = new DiseaseReportValue(locDis.Item1, locDis.Item2);
                if (!Values.Contains(reportValue))
                    Values.Add(reportValue);
                else findReportValue(reportValue).Count += 1;
            }       
        }

        DiseaseReportValue findReportValue(DiseaseReportValue value) =>
            Values.Find((pr => pr.Equals(value)));
    }
}
