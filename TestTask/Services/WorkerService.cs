using TestTask.Data;
using TestTask.Models;

namespace TestTask.Services
{
    public partial class WorkerService : IWorkerService
    {
        private readonly TestTaskContext _context;
        public WorkerService(TestTaskContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void DeleteDivision(int id)
        {
            if (!_divisionByIdDict.TryGetValue(id, out var division))
                throw new ArgumentException($"Division with id={id} not exist");

            _context.Division.Remove(division);
            _context.SaveChanges();
            _divisionByIdDict.Remove(division.Id);
        }

        public void DeleteWorker(int id)
        {
            if (!_workersByIdDict.TryGetValue(id, out var worker))
                throw new ArgumentException($"Worker with id={id} not exist");

            _context.Worker.Remove(worker);
            _context.SaveChanges();
            _workersByIdDict.Remove(worker.Id);
        }

        public void SaveDivision(Division division)
        {
            if (division is null)
                throw new ArgumentNullException(nameof(division));

            if (division.Id == 0)
                _context.Division.Add(division);
            else
                UpdateModelInCache(division);

            _context.SaveChanges();
        }

        public void SaveWorker(Worker worker)
        {
            if (worker is null)
                throw new ArgumentNullException(nameof(worker));

            if (worker.Id == 0)
                _context.Worker.Add(worker);
            else
                UpdateModelInCache(worker);

            _context.SaveChanges();
        }

        public Division? GetDivision(int id)
        {
            _divisionByIdDict.TryGetValue(id, out var division);
            return division;
        }

        public List<Division> GetDivisions()
        {
            return _divisionByIdDict.Values.ToList();
        }

        public Worker? GetWorker(int id)
        {
            _workersByIdDict.TryGetValue(id, out var worker);
            return worker;
        }

        public List<Worker> GetWorkers()
        {
            return _workersByIdDict.Values.ToList();
        }

        public List<Worker> GetAllWorkersByDivision(Division division)
        {
            var _childIds = new List<int>();

            FillRecursive(division);

            return GetWorkers().FindAll(x => _childIds.Contains(x.DivisionId) || x.DivisionId == division.Id);

            void FillRecursive(Division division)
            {
                foreach (var child in division.Children)
                {
                    _childIds.Add(child.Id);
                    FillRecursive(child);
                }
            }
        }
    }
}