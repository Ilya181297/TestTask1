using TestTask.Models;

namespace TestTask.Services
{
    public partial class WorkerService
    {
        private Dictionary<int, Worker> _workersByIdDict = new Dictionary<int, Worker>();
        private Dictionary<int, Division> _divisionByIdDict = new Dictionary<int, Division>();

        public void InitializeCache()
        {
            _workersByIdDict = _context.Worker.ToDictionary(x => x.Id);
            _divisionByIdDict = _context.Division.ToDictionary(x => x.Id);
        }

        private void UpdateModelInCache(Worker worker)
        {
            if (!_workersByIdDict.TryGetValue(worker.Id, out var workerInCache))
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
            if (!_divisionByIdDict.TryGetValue(division.Id, out var divisionInCache))
                throw new ArgumentException($"Division with Id={division.Id} does not exist in cache");

            divisionInCache.Name = division.Name;
            divisionInCache.FormationDate = division.FormationDate;
            divisionInCache.Description = division.Description;
            divisionInCache.ParentId = division.ParentId;
        }
    }
}