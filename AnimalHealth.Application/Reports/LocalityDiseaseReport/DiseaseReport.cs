using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.BasicReportEntities;

namespace AnimalHealth.Application.Reports.LocalityDiseaseReport
{
    public class DiseaseReport
    {
        public int Id { get; set; }
        public ReportState State { get; set; }
        public string User { get; set; }
        DateTime createDate;
        public DateTime CreateDate
        {
            get => createDate.ToUniversalTime();
            set => createDate = value;
        }
        public string Type { get => "Отчёт по болезням"; }    
        public List<DiseaseReportValue> Values { get; set; } 

        public DiseaseReport()
        {
            Values = new List<DiseaseReportValue>();
            State = ReportState.Created;
            CreateDate = DateTime.Now;
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
