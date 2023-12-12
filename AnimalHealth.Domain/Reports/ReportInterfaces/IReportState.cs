using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public interface IReportState
    {
        /// <summary>
        /// Дата присвоения состояния.
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Пользователь, изменивший состояние отчёта.
        /// </summary>
        User Changer { get; set; }

        /// <summary>
        /// Название состояния.
        /// </summary>
        string Name { get; }   

        /// <summary>
        /// Утвердить отчёт.
        /// </summary>
        /// <param name="report">Утверждаемый отчёт.</param>
        void Approve(Report report, DateTime date, User user);

        /// <summary>
        /// Отправить отчёт.
        /// </summary>
        /// <param name="report">Отправляемый отчёт</param>
        void Send(Report report, DateTime date, User user);

        /// <summary>
        /// Отменить утверждение отчёта.
        /// </summary>
        /// <param name="report">Отменяемый отчёт</param>
        void Cancel(Report report, DateTime date, User user);
    }
}
