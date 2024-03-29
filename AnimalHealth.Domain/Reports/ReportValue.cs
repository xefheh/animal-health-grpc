﻿namespace AnimalHealth.Domain.Reports
{
    public class ReportValue
    {
        public int Id { get; set; }
        public string FirstFeature { get; set; }
        public string SecondFeature { get; set; }
        public int Count { get; set; }
        public ReportValue(string firstFeature, string secondFeature) =>
            (FirstFeature, SecondFeature, Count) = (firstFeature, secondFeature, 1);

        public ReportValue() { }

        public override bool Equals(object? obj)
        {
            return obj != null &&
                obj is ReportValue objR &&
                objR.FirstFeature == FirstFeature &&
                objR.SecondFeature == SecondFeature;
        }
    }
}