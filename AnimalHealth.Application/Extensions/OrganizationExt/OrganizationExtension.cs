using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Application.Extensions.OrganizationExt;

public static class OrganizationExtension
{
    /// <summary>
    /// Обновление полей существующего объекта.
    /// </summary>
    /// <param name="oldOrganization">Существующий объект.</param>
    /// <param name="updatedOrganization">Объект с новыми полями.</param>
    public static void UpdateFields(this Organization oldOrganization, Organization updatedOrganization)
    {
        oldOrganization.Trc = updatedOrganization.Trc;
        oldOrganization.Feature = updatedOrganization.Feature;
        oldOrganization.Locality = updatedOrganization.Locality;
        oldOrganization.Type = updatedOrganization.Type;
        oldOrganization.Name = updatedOrganization.Name;
    }
}