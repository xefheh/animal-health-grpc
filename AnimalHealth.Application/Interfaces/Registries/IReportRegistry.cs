using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;

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
        public Task<DbSaveCondition> DeleteReportAsync(ReportLookup lookup, CancellationToken cancellationToken);

        /// <summary>
        /// Изменить состояние отчёта по айди.
        /// </summary>
        /// <param name="message">Состояние отчёта и его Id</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Id отчёта.</returns>
        public Task<ReportLookup> ChangeReportStateAsync(ReportStateModel message, CancellationToken cancellationToken);

        /// <summary>
        /// Получить данные, включащие названия отчётов, и их свойств, об отчётах.
        /// </summary>
        /// <param name="request">Пустой запрос</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Данные об отчётах</returns>
        public Task<ReportMetaData> GetReportMetaDataAsync(Empty request, CancellationToken cancellationToken);
    }
}