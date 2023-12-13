namespace AnimalHealth.Application.Exceptions
{
    public class IncorretReportStateException : Exception
    {
        public IncorretReportStateException(string message) : base(message) { }
    }
}
