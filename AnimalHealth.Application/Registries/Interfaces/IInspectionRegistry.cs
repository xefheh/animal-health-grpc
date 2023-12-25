using AnimalHealth.Application.Models;

namespace AnimalHealth.Application.Registries.Interfaces;

public interface IInspectionRegistry
{
    /// <summary>
    /// Получение осмотра по ключевому полю.
    /// </summary>
    /// <param name="lookup">Поисковая модель с ключевым полем.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>gRPC модель осмотра.</returns>
    public Task<InspectionModel> GetInspectionAsync(InspectionLookup lookup, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка осмотров.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список gRPC моделей осмотра</returns>
    public Task<InspectionModelList> GetInspectionsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Добавление нового осмотра.
    /// </summary>
    /// <param name="addedInspection">gRPC модель осмотра для добавления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Поисковое представление осмотра</returns>
    public Task<InspectionLookup> AddInspectionAsync(InspectionAddModel addedInspection,
        CancellationToken cancellationToken);

    /// <summary>
    /// Изменить поля существующего осмотра.
    /// </summary>
    /// <param name="updatedInspection">gRPC модель осмотра с обновлёнными полями.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Состояние сохранения БД.</returns>
    public Task<DbSaveCondition> UpdateInspectionAsync(InspectionModel updatedInspection,
        CancellationToken cancellationToken);

    /// <summary>
    /// Удалить осмотр по ключевому полю.
    /// </summary>
    /// <param name="lookup">Поисковая модель с ключевым полем.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Состояние сохранение БД.</returns>
    public Task<DbSaveCondition> DeleteInspectionAsync(InspectionLookup lookup, CancellationToken cancellationToken);

    /// <summary>
    /// Получить отчёт с группировкой по населённому пункту и типу животному, и количеством.
    /// </summary>
    /// <param name="dates">период отчёта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>gRPC модель отчёта</returns>
    public Task<ReportModel> GetAnimalTypeReportAsync(GetReport dates, CancellationToken cancellationToken);

    /// <summary>
    /// Получить отчёт с группировкой по населённому пункту и болезням, и количеством.
    /// </summary>
    /// <param name="dates">период отчёта</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>gRPC модель отчёта</returns>
    public Task<ReportModel> GetDiseaseReportAsync(GetReport dates, CancellationToken cancellationToken);
}