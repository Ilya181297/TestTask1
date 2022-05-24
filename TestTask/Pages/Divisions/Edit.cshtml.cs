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
        private readonly IWorkerService _workerService;

        private readonly ILogger<EditModel> _logger;
        public EditModel(IWorkerService workerService, ILogger<EditModel> logger)
        {
            _workerService = workerService;
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
                Divisions = PageHelper.ConvertToSelectList(_workerService
                .GetDivisions().Where(x => x.Id != id), true);

                if (id == 0)
                {
                    Division = new Division();
                    Division.FormationDate = DateTime.Today;

                    return Page();
                }

                Division = _workerService.GetDivision(id);

                if (Division is null)
                    return NotFound();

                SelectedParentId = Division.ParentId ?? 0;

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the method Divisions/Edit/OnGet");

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
                _workerService.SaveDivision(Division);

                return RedirectToPage("../Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the method Divisions/Edit/OnPost");

                return Page();
            }
        }
    }
}

