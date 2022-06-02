using TestTask.Models;

namespace TestTask.Services
{
    public partial class TestTaskService
    {
        private Dictionary<int, Worker> _workersDict = new();
        private Dictionary<int, Division> _divisionDict = new();

        public void InitializeCache()
        {
            _workersDict = _context.Worker.ToDictionary(x => x.Id);
            _divisionDict = _context.Division.ToDictionary(x => x.Id);
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
                _workersDict.Remove(workerId);
        }
    }
}