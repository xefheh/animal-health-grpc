using AnimalHealth.Application.Models;

namespace AnimalHealth.Application.Interfaces.Registries;

public interface IVaccinationRegistry
{
    /// <summary>
    /// Получение вакцинации по ключевому полю.
    /// </summary>
    /// <param name="lookup">Поисковая модель с ключевым полем.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>gRPC модель вакцинации.</returns>
    public Task<VaccinationModel> GetVaccinationAsync(VaccinationLookup lookup, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение списка вакцинаций.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список gRPC моделей вакцинаций</returns>
    public Task<VaccinationModelList> GetVaccinationsAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавление новой вакцинации.
    /// </summary>
    /// <param name="addedVaccination">gRPC модель вакцинации для добавления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Поисковая модель вакцинации</returns>
    public Task<VaccinationLookup> AddVaccinationAsync(VaccinationAddModel addedVaccination, CancellationToken cancellationToken);
    
    /// <summary>
    /// Изменить поля существующей вакцинации.
    /// </summary>
    /// <param name="updatedInspection">gRPC модель вакцинации с обновлёнными полями.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Состояние сохранения БД.</returns>
    public Task<DbSaveCondition> UpdateVaccinationAsync(VaccinationModel updatedInspection, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить вакцинацию по ключевому полю.
    /// </summary>
    /// <param name="lookup">Поисковая модель с ключевым полем.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Состояние сохранение БД.</returns>
    public Task<DbSaveCondition> DeleteVaccinationAsync(VaccinationLookup lookup, CancellationToken cancellationToken);
}