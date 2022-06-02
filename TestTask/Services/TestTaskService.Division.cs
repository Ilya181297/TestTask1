using TestTask.Models;

namespace TestTask.Services
{
    public partial class TestTaskService
    {
        public Division? GetDivision(int id)
        {
            _divisionDict.TryGetValue(id, out var division);
            return division;
        }

        public List<Division> GetDivisions()
        {
            return _divisionDict.Values.ToList();
        }

        public void SaveDivision(Division division)
        {
            lock (_syncRoot)
            {
                if (division is null)
                    throw new ArgumentNullException(nameof(division));

                var operations = new GenericOperations<Division>(_context);

                operations.Save(division, UpdateModelInCache
                    , _divisionDict.Add, _context.Division.Add);
            }
        }

        public void DeleteDivision(int id)
        {
            lock (_syncRoot)
            {
                if (!_divisionDict.TryGetValue(id, out var division))
                    throw new ArgumentException($"Division with id={id} not exist");

                var workerIds = GetAllWorkersByDivision(division).Select(x => x.Id);

                DeleteRecursive(division);
                DeleteWorkersFromCache(workerIds);
            }
        }

        private void DeleteRecursive(Division division)
        {
            foreach (var childDivision in division.Children)
                DeleteRecursive(childDivision);

            DeleteFromDbAndCache(division);
        }

        private void DeleteFromDbAndCache(Division division)
        {
            if (!_divisionDict.ContainsKey(division.Id))
                throw new ArgumentException($"Division with id={division.Id} not exist");

            var operations = new GenericOperations<Division>(_context);
            operations.Delete(division, _context.Division.Remove, _divisionDict.Remove);
        }
    }
}
