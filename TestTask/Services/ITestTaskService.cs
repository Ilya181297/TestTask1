using TestTask.Models;

namespace TestTask.Services
{
    public interface ITestTaskService
    {
        /// <summary>
        /// Инициализация кэша сервиса
        /// </summary>
        void InitializeCache();

        /// <summary>
        /// Возвращяет всех работников
        /// </summary>
        List<Worker> GetWorkers();

        /// <summary>
        /// Возвращяет всех работников конкретного подразделения
        /// </summary>
        /// <param name="division">Подразделение</param>
        /// <exception cref="ArgumentNullException">Division is null</exception>
        List<Worker> GetAllWorkersByDivision(Division division);

        /// <summary>
        /// Возвращает работника
        /// </summary>
        /// <param name="id">Идентификатор работника</param>
        Worker? GetWorker(int id);

        /// <summary>
        /// Добавление/редактирование работника
        /// </summary>
        /// <param name="worker">Работник</param>
        void SaveWorker(Worker worker);

        /// <summary>
        /// Удаление работника
        /// </summary>
        /// <param name="id">Идентификатор работника</param>
        /// <exception cref="ArgumentNullException">Worker is null</exception>
        void DeleteWorker(int id);

        /// <summary>
        /// Возвращает все подразделения
        /// </summary>
        List<Division> GetDivisions();

        /// <summary>
        /// Возваращет подразделение
        /// </summary>
        /// <param name="id">Идентификатор подразделения</param>
        Division? GetDivision(int id);

        /// <summary>
        /// Добавление/редактирование подразделения
        /// </summary>
        /// <param name="division">Подразделение</param>
        void SaveDivision(Division division);

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="id">Идентификатор подразделения</param>
        /// <exception cref="ArgumentNullException">Division is null</exception>
        void DeleteDivision(int id);
    }
}
