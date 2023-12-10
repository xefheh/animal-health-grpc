namespace AnimalHealth.Application.Exceptions
{
    public class IncorrectChangeReportStateException : Exception
    {
        public IncorrectChangeReportStateException(string message) : base(message) { }
    }
}
