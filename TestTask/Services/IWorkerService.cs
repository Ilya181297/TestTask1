using TestTask.Models;

namespace TestTask.Services
{
    public interface IWorkerService
    {
        void DeleteWorker(int id);
        void DeleteDivision(int id);
        void SaveWorker(Worker worker);
        void SaveDivision(Division division);
        Worker? GetWorker(int id);
        Division? GetDivision(int id);
        List<Worker> GetWorkers();
        List<Division> GetDivisions();
        void InitializeCache();
        List<Worker> GetAllWorkersByDivision(Division division);
    }
}
