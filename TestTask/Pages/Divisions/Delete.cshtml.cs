#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Divisions
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
        public Division Division { get; set; }

        public IActionResult OnGet(int id)
        {
            try
            {
                Division = _companyService.GetDivision(id);

                if (Division is null)
                    return NotFound($"Division with Id={id} is not found");

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PageHelper.GetErrorMessage("Divisions/Delete/OnGet"));

                return Page();
            }
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                _companyService.DeleteDivision(id);

                return RedirectToPage("../Index", new { id = PageHelper.SelectedDivisionIdOnFilter });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PageHelper.GetErrorMessage("Divisions/Delete OnPost"));

                return Page();
            }
        }
    }
}
