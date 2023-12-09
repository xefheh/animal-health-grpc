using AnimalHealth.Domain.BasicReportEntities;

namespace AnimalHealth.Application.Reports.LocalityDiseaseReport
{
    public class DiseaseReportValue
    {
        public int Id { get; set; }
        public string Locality { get; set; }
        public string Disease { get; set; }
        public int Count { get; set; }

        public DiseaseReportValue(string locality, string disease) =>
            (Locality, Disease, Count) = (locality, disease, 1);

        public DiseaseReportValue() {  } 
    }
}
