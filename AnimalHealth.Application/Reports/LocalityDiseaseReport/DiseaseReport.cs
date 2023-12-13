using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class DiseaseReport : Report
    {
        public override string Type { get => "По нас.пункту и болезням"; }
        public DiseaseReport() : base() { }

    }
}
