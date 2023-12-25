using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Domain.Reports
{
    public interface IReportState
    {
        /// <summary>
        /// Дата присвоения состояния.
        /// </summary>
        DateTime Date { get; set; }

        string DateName { get; }
        /// <summary>
        /// Пользователь, изменивший состояние отчёта.
        /// </summary>
        User Changer { get; set; }
        
        string ChangerName { get; }

        /// <summary>
        /// Второй пользователь, изменивший состояние отчёта.
        /// </summary>
        User AdditionalChanger { get; set; }

        string AdditionalChangerName { get; }

        /// <summary>
        /// Название состояния.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Обработать изменение состояние отчёта.
        /// </summary>
        /// <param name="report">Изменяемый отчёт.</param>
        /// <param name="state">Новое состояние отчёта.</param>
        void Handle(Report report, DateTime date, List<User> users);
    }
}
