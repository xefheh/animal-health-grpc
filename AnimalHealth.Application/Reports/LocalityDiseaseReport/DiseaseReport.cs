using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class DiseaseReport : Report
    {
        public override string Type { get => "ОТчтёт"; }
        public DiseaseReport() : base() { }

    }
}
