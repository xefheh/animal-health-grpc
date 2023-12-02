using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Application.Extensions.InspectionExt;

public static class InspectionExtension
{
    /// <summary>
    /// Обновление полей существующего объекта.
    /// </summary>
    /// <param name="oldInspection">Существующий объект.</param>
    /// <param name="updatedInspection">Объект с новыми полями.</param>
    public static void UpdateFields(this Inspection oldInspection, Inspection updatedInspection)
    {
        oldInspection.FeatureBehaviour = updatedInspection.FeatureBehaviour;
        oldInspection.AnimalCondition = updatedInspection.AnimalCondition;
        oldInspection.Temperature = updatedInspection.Temperature;
        oldInspection.SkinCover = updatedInspection.SkinCover;
        oldInspection.FurCondition = updatedInspection.FurCondition;
        oldInspection.Injures = updatedInspection.Injures;
        oldInspection.IsNeedOperations = updatedInspection.IsNeedOperations;
        oldInspection.Manipulations = updatedInspection.Manipulations;
        oldInspection.Treatment = updatedInspection.Treatment;
        oldInspection.Date = updatedInspection.Date;
        oldInspection.Doctor = updatedInspection.Doctor;
        oldInspection.InspectedAnimal = updatedInspection.InspectedAnimal;
        oldInspection.Contract = updatedInspection.Contract;
        oldInspection.Disease = updatedInspection.Disease;
    }
}