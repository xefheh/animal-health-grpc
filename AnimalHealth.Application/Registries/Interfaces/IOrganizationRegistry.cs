using AnimalHealth.Application.Models;

namespace AnimalHealth.Application.Registries.Interfaces;

public interface IOrganizationRegistry
{
    /// <summary>
    /// Получение организации по ключевому полю.
    /// </summary>
    /// <param name="lookup">Поисковая модель с ключевым полем.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>gRPC модель организации.</returns>
    public Task<OrganizationModel> GetOrganizationAsync(OrganizationLookup lookup, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение списка организаций.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список gRPC моделей организаций.</returns>
    public Task<OrganizationModelList> GetOrganizationsAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавление новой организации.
    /// </summary>
    /// <param name="addedOrganization">gRPC модель организации для добавления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Поисковая модель организации</returns>
    public Task<OrganizationLookup> AddOrganizationAsync(OrganizationAddModel addedOrganization, CancellationToken cancellationToken);
    
    /// <summary>
    /// Изменить поля существующего организации.
    /// </summary>
    /// <param name="updatedOrganization">gRPC модель организации с обновлёнными полями.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Состояние сохранения БД.</returns>
    public Task<DbSaveCondition> UpdateOrganizationAsync(OrganizationModel updatedOrganization, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить организацию по ключевому полю.
    /// </summary>
    /// <param name="lookup">Поисковая модель с ключевым полем.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Состояние сохранение БД.</returns>
    public Task<DbSaveCondition> DeleteOrganizationAsync(OrganizationLookup lookup, CancellationToken cancellationToken);
}