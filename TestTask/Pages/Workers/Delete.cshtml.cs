#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Workers
{
    public class DeleteModel : PageModel
    {
        private readonly ICompanyService _companyService;

        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ICompanyService companyService, ILogger<DeleteModel> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [BindProperty]
        public Worker Worker { get; set; }

        public IActionResult OnGet(int id)
        {
            try
            {
                Worker = _companyService.GetWorker(id);

                if (Worker is null)
                    return NotFound($"Worker with Id={id} is not found");

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PageHelper.GetErrorMessage("Workers/Delete/OnGet"));

                return Page();
            }
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                _companyService.DeleteWorker(id);

                return RedirectToPage("../Index", new { id = PageHelper.SelectedDivisionIdOnFilter });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PageHelper.GetErrorMessage("Workers/Index/OnPost"));

                return Page();
            }
        }
    }
}
