using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.BasicReportEntities;

namespace AnimalHealth.Application.Reports.LocalityAnimalTypeReport
{
    public class AnimalTypeReport 
    {
        public int Id { get; set; }
        public ReportState State { get; set; }
        public string User { get; set; }
        public DateTime CreateDate { get; set; }
        public string Type { get => "Отчёт по типам животным"; }
        public List<AnimalTypeReportValue> Values { get; set; }

        public AnimalTypeReport()
        {
            Values = new List<AnimalTypeReportValue>();
            State = ReportState.Created;
            CreateDate = DateTime.Now;
        }

        public void GetReport(ICollection<Inspection> vaccinations)
        {
            foreach (var vaccination in vaccinations)
            {
                var locDis = vaccination.GetLocalityAnimalType();
                var reportValue = new AnimalTypeReportValue(locDis.Item1, locDis.Item2);
                if (!Values.Contains(reportValue))
                    Values.Add(reportValue);
                else findReportValue(reportValue).Count += 1;
            }
        }

        AnimalTypeReportValue findReportValue(AnimalTypeReportValue value) =>
            Values.Find((pr => pr.Equals(value)));
    }
}
