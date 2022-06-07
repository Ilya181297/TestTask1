using TestTask.Data;
using TestTask.Models;

namespace TestTask.Services
{
    public partial class TestTaskService : ITestTaskService
    {
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
            if (worker is null)
                throw new ArgumentNullException(nameof(worker));

            lock (_syncRoot)
            {
                var crud = new GenericOperations<Worker>(_context);
                crud.Save(worker, UpdateModelInCache
                    , _workersDict.TryAdd, _context.Worker.Add);
            }
        }

        public void DeleteWorker(int id)
        {
            if (!_workersDict.TryGetValue(id, out var worker))
                throw new ArgumentException($"Worker with id={id} not exist");

            lock (_syncRoot)
            {
                // не выкидываем исключение, т.к. в данном случае другой поток уже успел удалить - не критично
                if (!_workersDict.TryGetValue(id, out worker))
                    return;

                _context.Worker.Remove(worker);
                _context.SaveChanges();
                _workersDict.TryRemove(id, out var _);
            }
        }
    }
}