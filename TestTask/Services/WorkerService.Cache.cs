using Microsoft.EntityFrameworkCore;
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
    }
}