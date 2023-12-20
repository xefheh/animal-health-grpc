using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Reports;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Interfaces.Registries
{
    public interface IReportRegistry
    {
        /// <summary>
        /// Добавить отчёт в БД;
        /// </summary>
        /// <param name="report">сохраняемый отчёт</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>состояние сохранение в БД</returns>
        public Task<ReportLookup> AddReportAsync(Report report, CancellationToken cancellationToken);

        /// <summary>
        /// Получение списка отчётов.
        /// </summary>
        /// <param name="request">Id отчёта</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>gRPC модель отчёта</returns>
        public Task<ReportModel> GetReportAsync(ReportLookup request, CancellationToken cancellationToken);

        /// <summary>
        /// Получение списка отчётов.
        /// </summary>
        /// <param name="period">Период дат, за которые нужно получить отчёты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список gRPC моделей отчётов</returns>
        public Task<ReportModelList> GetReportsByPeriodAsync(DatesPeriod period, CancellationToken cancellationToken);

        /// <summary>
        /// Получение списка отчётов.
        /// </summary>
        /// <param name="user">Пользователь, который создал получаемые отчёты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список gRPC моделей отчётов</returns>
        public Task<ReportModelList> GetReportsByUserAsync(UserModel user, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить отчёт по ключевому полю.
        /// </summary>
        /// <param name="lookup">Поисковая модель с ключевым полем.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Id отчёта.</returns>
        public Task<ReportLookup> DeleteReportAsync(ReportLookup lookup, CancellationToken cancellationToken);

        /// <summary>
        /// Утвердить отчёт.
        /// </summary>
        /// <param name="request">Дата утверждения, пользователь и Id отчёта</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Id отчёта.</returns>
        public Task<ReportLookup> GoNextStateAsync(ChangeReportState request, CancellationToken cancellationToken);

        /// <summary>
        /// Отправить отчёт.
        /// </summary>
        /// <param name="request">Дата отправления, пользователь, получатель и Id отчёта</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Id отчёта.</returns>
        public Task<ReportLookup> SendReportAsync(ChangeReportState request, CancellationToken cancellationToken);

        /// <summary>
        /// Получить данные, включащие названия отчётов, и их свойств, об отчётах.
        /// </summary>
        /// <param name="request">Пустой запрос</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Данные об отчётах</returns>
        public Task<ReportMetaData> GetReportMetaDataAsync(Empty request, CancellationToken cancellationToken);
    }
}