using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class VaccinationReport : Report
    {
        public override string Type { get => "VaccinationReport"; }
        public VaccinationReport() : base() { }
    }
}