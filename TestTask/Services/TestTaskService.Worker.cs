using TestTask.Data;
using TestTask.Models;

namespace TestTask.Services
{
    public partial class TestTaskService : ITestTaskService
    {
        private readonly TestTaskContext _context;
        private readonly object _syncRoot = new();

        public TestTaskService(TestTaskContext context)
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
            lock (_syncRoot)
            {
                if (worker is null)
                    throw new ArgumentNullException(nameof(worker));

                var crud = new GenericOperations<Worker>(_context);

                crud.Save(worker, UpdateModelInCache
                    , _workersDict.Add, _context.Worker.Add);
            }
        }

        public void DeleteWorker(int id)
        {
            lock (_syncRoot)
            {
                if (!_workersDict.TryGetValue(id, out var worker))
                    throw new ArgumentException($"Worker with id={id} not exist");

                var operations = new GenericOperations<Worker>(_context);
                operations.Delete(worker, _context.Worker.Remove, _workersDict.Remove);
            }
        }
    }
}