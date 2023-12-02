using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Application.Extensions.VaccinationExt;

public static class VaccinationExtension
{
    /// <summary>
    /// Обновление полей существующего объекта.
    /// </summary>
    /// <param name="oldVaccination">Существующий объект.</param>
    /// <param name="updatedVaccination">Объект с новыми полями.</param>
    public static void UpdateFields(this Vaccination oldVaccination, Vaccination updatedVaccination)
    {
        oldVaccination.Date = updatedVaccination.Date;
        oldVaccination.ExpirationDate = updatedVaccination.ExpirationDate;
        oldVaccination.Vaccine = updatedVaccination.Vaccine;
        oldVaccination.User = updatedVaccination.User;
        oldVaccination.Animal = updatedVaccination.Animal;
        oldVaccination.Contract = updatedVaccination.Contract;
    }
}