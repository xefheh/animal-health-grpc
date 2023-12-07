using AnimalHealth.Application.Models;

namespace AnimalHealth.Application.Interfaces.Registries
{
    public interface IReportRegistry
    {
        /// <summary>
        /// Получение списка отчётов.
        /// </summary>
        /// <param name="user">пользователь, который создал отчёт.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список gRPC моделей отчётов</returns>
        public Task<ReportModelList> GetReportsAsync(ReportUserName user, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить отчёт по ключевому полю.
        /// </summary>
        /// <param name="lookup">Поисковая модель с ключевым полем.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Состояние сохранение БД.</returns>
        public Task<DbSaveCondition> DeleteContractAsync(ReportLookup lookup, CancellationToken cancellationToken);
    }
}
