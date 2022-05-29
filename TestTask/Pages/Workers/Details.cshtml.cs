#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Workers
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
                _logger.LogError(ex, PageHelper.GetErrorMessage("Workers/Details/OnGet"));

                return Page();
            }
        }
    }
}
