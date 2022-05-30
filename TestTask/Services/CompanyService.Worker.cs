using TestTask.Data;
using TestTask.Models;

namespace TestTask.Services
{
    public partial class CompanyService : ICompanyService
    {
        private readonly TestTaskContext _context;

        public CompanyService(TestTaskContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Worker> GetWorkers()
        {
            return _workersDict.Values.ToList();
        }

        public List<Worker> GetAllWorkersByDivision(Division division)
        {
            if (division is null)
                throw new ArgumentNullException(nameof(division));

            return GetWorkers().FindAll(x => x.IsIncludeInDivision(division.Id));
        }

        public Worker? GetWorker(int id)
        {
            _workersDict.TryGetValue(id, out var worker);
            return worker;
        }

        public void SaveWorker(Worker worker)
        {
            if (worker is null)
                throw new ArgumentNullException(nameof(worker));

            if (worker.Id == 0)
                Add(worker);
            else
                Edit(worker);
        }

        public void DeleteWorker(int id)
        {
            if (!_workersDict.TryGetValue(id, out var worker))
                throw new ArgumentException($"Worker with id={id} not exist");

            _context.Worker.Remove(worker);
            _context.SaveChanges();
            _workersDict.Remove(worker.Id);
        }

        private void Edit(Worker worker)
        {
            UpdateModelInCache(worker);
            _context.SaveChanges();
        }

        private void Add(Worker worker)
        {
            _context.Worker.Add(worker);
            _context.SaveChanges();
            _workersDict.Add(worker.Id, worker);
        }
    }
}