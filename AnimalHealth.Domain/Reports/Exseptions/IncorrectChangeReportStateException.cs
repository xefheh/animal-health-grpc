namespace AnimalHealth.Domain.Reports
{
    public class IncorrectChangeReportStateException : Exception
    {
        public IncorrectChangeReportStateException(string message) : base(message) { }
    }
}
