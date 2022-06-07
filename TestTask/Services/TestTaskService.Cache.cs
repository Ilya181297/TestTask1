using System.Collections.Concurrent;
using TestTask.Models;

namespace TestTask.Services
{
    public partial class TestTaskService
    {
        private ConcurrentDictionary<int, Worker> _workersDict = new();
        private ConcurrentDictionary<int, Division> _divisionDict = new();

        public void InitializeCache()
        {
            var workers = _context.Worker.Select(x => new KeyValuePair<int, Worker>(x.Id, x));
            _workersDict = new ConcurrentDictionary<int, Worker>(workers);

            var divisions = _context.Division.Select(x => new KeyValuePair<int, Division>(x.Id, x));
            _divisionDict = new ConcurrentDictionary<int, Division>(divisions);
        }

        private void UpdateModelInCache(Worker worker)
        {
            if (!_workersDict.TryGetValue(worker.Id, out var workerInCache))
                throw new ArgumentException($"Worker with Id={worker.Id} does not exist in cache");

            workerInCache.Name = worker.Name;
            workerInCache.Surname = worker.Surname;
            workerInCache.MiddleName = worker.Surname;
            workerInCache.IsHasDriveLicense = worker.IsHasDriveLicense;
            workerInCache.BirthDate = worker.BirthDate;
            workerInCache.DivisionId = worker.DivisionId;
            workerInCache.Post = worker.Post;
            workerInCache.GenderId = worker.GenderId;
        }
        private void UpdateModelInCache(Division division)
        {
            if (!_divisionDict.TryGetValue(division.Id, out var divisionInCache))
                throw new ArgumentException($"Division with Id={division.Id} does not exist in cache");

            divisionInCache.Name = division.Name;
            divisionInCache.FormationDate = division.FormationDate;
            divisionInCache.Description = division.Description;
            divisionInCache.ParentId = division.ParentId;
        }
        private void DeleteWorkersFromCache(IEnumerable<int> workerIds)
        {
            foreach (var workerId in workerIds)
                _workersDict.TryRemove(workerId, out Worker? worker);
        }
    }
}