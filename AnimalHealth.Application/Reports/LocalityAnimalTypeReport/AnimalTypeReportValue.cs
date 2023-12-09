﻿namespace AnimalHealth.Application.Reports.LocalityAnimalTypeReport
{
    public class AnimalTypeReportValue 
    {
        public int Id { get; set; }
        public string Locality { get; set; }
        public string AnimalType { get; set; }
        public int Count { get; set; }

        public AnimalTypeReportValue(string locality, string animalType) =>
            (Locality, AnimalType, Count) = (locality, animalType, 1);

        public AnimalTypeReportValue() { }

        public override bool Equals(object? obj)
        {
            return obj != null &&
                obj is AnimalTypeReportValue objR &&
                objR.Locality == Locality &&
                objR.AnimalType == AnimalType;
        }
    }
}
