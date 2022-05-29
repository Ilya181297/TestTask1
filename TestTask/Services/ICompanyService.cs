using TestTask.Models;

namespace TestTask.Services
{
    public interface ICompanyService
    {
        void InitializeCache();

        List<Worker> GetWorkers();
        List<Worker> GetAllWorkersByDivision(Division division);
        Worker? GetWorker(int id);
        void SaveWorker(Worker worker);
        void DeleteWorker(int id);

        List<Division> GetDivisions();
        Division? GetDivision(int id);
        void SaveDivision(Division division);
        void DeleteDivision(int id);
    }
}
