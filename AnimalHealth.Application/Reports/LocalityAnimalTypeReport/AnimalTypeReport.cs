using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class AnimalTypeReport : Report
    {
        public override string Type { get => "ОТчтёт"; }
        public AnimalTypeReport() : base() { }
    }
}
