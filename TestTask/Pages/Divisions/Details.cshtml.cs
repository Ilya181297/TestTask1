#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Divisions
{
    public class DetailsModel : PageModel
    {
        private readonly ICompanyService _companyService;

        private readonly ILogger<DetailsModel> _logger;
        public DetailsModel(ICompanyService companyService, ILogger<DetailsModel> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }
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
                _logger.LogError(ex, PageHelper.GetErrorMessage("Divisions/Details/OnGet"));

                return Page();
            }
        }
    }
}
