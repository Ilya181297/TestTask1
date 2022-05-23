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
        private readonly IWorkerService _workerService;

        private readonly ILogger<EditModel> _logger;
        public EditModel(IWorkerService workerService, ILogger<EditModel> logger)
        {
            _workerService = workerService;
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
                Divisions = PageHelper.ConvertToSelectList(_workerService.GetDivisions());

                if (id == 0)
                {
                    Worker = new Worker();

                    return Page();
                }

                Worker = _workerService.GetWorker(id);

                if (Worker is null)
                    return NotFound();

                SelectedDivisionId = Worker.DivisionId;
                SelectedGender = Worker.GenderId;

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the method Workers/Edit/OnGet");

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

                _workerService.SaveWorker(Worker);

                return RedirectToPage("../Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the method Workers/Edit/OnPost");

                return Page();
            }
        }
    }
}
