#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Divisions
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

        [BindProperty]
        public Division Division { get; set; }

        [BindProperty]
        public int SelectedParentId { get; set; }

        public IActionResult OnGet(int id)
        {
            try
            {
                var divisions = _companyService.GetDivisions().Where(x => x.Id != id);
                Divisions = PageHelper.ConvertToSelectList(divisions, true);

                if (id == 0)
                {
                    Division = new Division();
                    Division.FormationDate = DateTime.Today;
                    SelectedParentId = PageHelper.SelectedDivisionIdOnFilter ?? 0;

                    return Page();
                }

                Division = _companyService.GetDivision(id);

                if (Division is null)
                    return NotFound();

                SelectedParentId = Division.ParentId ?? 0;

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PageHelper.GetErrorMessage("Divisions/Edit/OnGet"));

                return Page();
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                Division.ParentId = SelectedParentId == 0 ? null : SelectedParentId;
                _companyService.SaveDivision(Division);

                return RedirectToPage("../Index", new { id = PageHelper.SelectedDivisionIdOnFilter});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PageHelper.GetErrorMessage("Divisions/Edit/OnPost"));

                return Page();
            }
        }
    }
}

