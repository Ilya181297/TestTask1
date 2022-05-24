#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Divisions
{
    public class DetailsModel : PageModel
    {
        private readonly IWorkerService _workerService;

        private readonly ILogger<DetailsModel> _logger;
        public DetailsModel(IWorkerService workerService, ILogger<DetailsModel> logger)
        {
            _workerService = workerService;
            _logger = logger;
        }
        public Division Division { get; set; }

        public IActionResult OnGet(int id)
        {
            try
            {
                Division = _workerService.GetDivision(id);

                if (Division is null)
                    return NotFound($"Division with Id={id} is not found");

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the method Divisions/Details/OnGet");

                return Page();
            }
        }
    }
}
