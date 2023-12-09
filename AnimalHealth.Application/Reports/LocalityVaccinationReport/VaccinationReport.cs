﻿using AnimalHealth.Application.Reports.LocalityDiseaseReport;
using AnimalHealth.Domain.BasicReportEntities;
using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Application.Reports.LocalityVaccinationReport
{
    public class VaccinationReport
    {
        public int Id { get; set; }
        public ReportState State { get; set; }
        public string User { get; set; }
        public DateTime CreateDate { get; set; }
        public string Type { get; set; }
        public List<VaccinationReportValue> Values { get; set; }

        public VaccinationReport()
        {
            Values = new List<VaccinationReportValue>();
            State = ReportState.Created;
        }

        public void GetReport(ICollection<Vaccination> vaccinations)
        {
            foreach (var vaccination in vaccinations)
            {
                var locDis = vaccination.GetLocalityVaccine();
                var reportValue = new VaccinationReportValue(locDis.Item1, locDis.Item2);
                if (!Values.Contains(reportValue))
                    Values.Add(reportValue);
                else findReportValue(reportValue).Count += 1;
            }
        }
        VaccinationReportValue findReportValue(VaccinationReportValue value) =>
           Values.Find((pr => pr.Equals(value)));
    }
}