#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Divisions
{
    public class DeleteModel : PageModel
    {
        private readonly IWorkerService _workerService;

        private readonly ILogger<DeleteModel> _logger;
        public DeleteModel(IWorkerService workerService, ILogger<DeleteModel> logger)
        {
            _workerService = workerService;
            _logger = logger;
        }

        [BindProperty]
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
                _logger.LogError(ex, "An error occurred in the method Divisions/Delete/OnGet");

                return Page();
            }
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                _workerService.DeleteDivision(id);

                return RedirectToPage("../Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the method Divisions/Delete/OnPost");

                return Page();
            }
        }
    }
}
