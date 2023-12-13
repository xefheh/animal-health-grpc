using AnimalHealth.Domain.Identity;
using AnimalHealth.Domain.Reports;


namespace AnimalHealth.Domain.Reports
{
    public class VaccinationReport : Report
    {
        public override string Type { get => "По нас.пункту и вакцинам"; }
        public VaccinationReport() : base() { }
    }
}