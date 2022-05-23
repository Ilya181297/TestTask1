#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWorkerService _workerService;

        private readonly ILogger<IndexModel> _logger;
        public IndexModel(IWorkerService workerService, ILogger<IndexModel> logger)
        {
            _workerService = workerService;
            _logger = logger;
        }

        public List<Division> Divisions { get; set; }
        public List<Worker> Workers { get; set; }
        public Division SelectedDivision { get; set; }

        public void OnGet()
        {
            try
            {
                Divisions = _workerService.GetDivisions().FindAll(x => !x.ParentId.HasValue);
                Workers = _workerService.GetWorkers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the method Index/OnGet");
            }
        }

        public IActionResult OnGetFilter(int id)
        {
            try
            {
                Divisions = _workerService.GetDivisions().FindAll(x => !x.ParentId.HasValue);

                SelectedDivision = _workerService.GetDivision(id);

                if (SelectedDivision is null)
                {
                    Workers = _workerService.GetWorkers();

                    return Page();
                }

                Workers = _workerService.GetAllWorkersByDivision(SelectedDivision);

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the method Index/OnGetFilter");

                return Page();
            }
        }
    }
}
