#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Workers
{
    public class EditModel : PageModel
    {
        private readonly ICompanyService _companyService;

        private readonly ILogger<EditModel> _logger;

        public EditModel(ICompanyService companyService, ILogger<EditModel> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }
        public List<SelectListItem> Divisions { get; set; }
        public List<SelectListItem> Genders { get; set; }

        [BindProperty]
        public int SelectedDivisionId { get; set; }

        [BindProperty]
        public int SelectedGender { get; set; }

        [BindProperty]
        public Worker Worker { get; set; }

        public IActionResult OnGet(int id)
        {
            try
            {
                Genders = PageHelper.GetGenderListItems();
                Divisions = PageHelper.ConvertToSelectList(_companyService.GetDivisions());

                if (id == 0)
                {
                    Worker = new Worker();
                    Worker.BirthDate = DateTime.Today.AddYears(-18);
                    SelectedDivisionId = PageHelper.SelectedDivisionIdOnFilter ?? 0;

                    return Page();
                }

                Worker = _companyService.GetWorker(id);

                if (Worker is null)
                    return NotFound($"Worker with Id={id} is not found");

                SelectedDivisionId = Worker.DivisionId;
                SelectedGender = Worker.GenderId;

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PageHelper.GetErrorMessage("Workers/Edit/OnGet"));

                return Page();
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                Worker.DivisionId = SelectedDivisionId;
                Worker.GenderId = SelectedGender;

                _companyService.SaveWorker(Worker);

                return RedirectToPage("../Index", new { id = PageHelper.SelectedDivisionIdOnFilter });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PageHelper.GetErrorMessage("Workers/Edit/OnPost"));

                return Page();
            }
        }
    }
}
