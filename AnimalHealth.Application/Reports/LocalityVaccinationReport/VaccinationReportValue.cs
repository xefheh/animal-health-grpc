using AnimalHealth.Application.Reports.LocalityDiseaseReport;
using AnimalHealth.Domain.BasicReportEntities;
using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Application.Reports.LocalityVaccinationReport
{
    public class VaccinationReportValue 
    {
        public int Id { get; set; }
        public string Locality { get; set; }
        public string Vaccine { get; set; }
        public int Count { get; set; }
        public VaccinationReportValue(string firstFeature, string secondFeature) =>
            (Locality, Vaccine, Count) = (firstFeature, secondFeature, 1);

        public VaccinationReportValue() { }

        public override bool Equals(object? obj)
        {
            return obj != null &&
                obj is VaccinationReportValue objR &&
                objR.Locality == Locality &&
                objR.Vaccine == Vaccine;
        }
    }
}
