namespace AnimalHealth.Domain.BasicReportEntities
{
    public class Report
    {
        public int Id { get; set; }
        public ReportState State { get; set; }
        public string User { get; set; }
        public DateTime CreateDate { get; set; }
        public string Type { get; set; }
        public List<ReportValue> Values { get; set; }

        public Report()
        {
            Values = new List<ReportValue>();
            State = ReportState.Created;
        }
    }
}
