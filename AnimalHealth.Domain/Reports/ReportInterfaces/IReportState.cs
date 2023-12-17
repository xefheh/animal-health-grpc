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
        /// Обработать изменение состояние отчёта.
        /// </summary>
        /// <param name="report">Изменяемый отчёт.</param>
        /// <param name="state">Новое состояние отчёта.</param>
        void Handle(Report report, User user, DateTime date);
    }
}
