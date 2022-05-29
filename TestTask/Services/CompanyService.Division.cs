using TestTask.Models;

namespace TestTask.Services
{
    public partial class CompanyService
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
            if (division is null)
                throw new ArgumentNullException(nameof(division));

            if (division.Id == 0)
                Add(division);
            else
                Edit(division);
        }

        public void DeleteDivision(int id)
        {
            if (!_divisionDict.TryGetValue(id, out var division))
                throw new ArgumentException($"Division with id={id} not exist");

            var workerIds = GetAllWorkersByDivision(division).Select(x => x.Id);

            DeleteRecursive(division);
            DeleteWorkersFromCache(workerIds);
        }

        private void DeleteRecursive(Division division)
        {
            foreach (var childDivision in division.Children)
                DeleteRecursive(childDivision);

            DeleteFromDbAndCache(division);
        }

        private void DeleteFromDbAndCache(Division division)
        {
            _context.Division.Remove(division);
            _context.SaveChanges();
            _divisionDict.Remove(division.Id);
        }

        private void Edit(Division division)
        {
            UpdateModelInCache(division);
            _context.SaveChanges();
        }

        private void Add(Division division)
        {
            _context.Division.Add(division);
            _context.SaveChanges();
            _divisionDict.Add(division.Id, division);
        }
    }
}
