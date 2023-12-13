using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public class AnimalTypeReport : Report
    {
        public override string Type { get => "По нас.пункту и типам животных"; }
        public AnimalTypeReport() : base() { }
    }
}
