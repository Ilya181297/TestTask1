#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Workers
{
    public class EditModel : PageModel
    {
        private readonly TestTaskContext _context;

        public List<SelectListItem> Divisions { get; set; }
        public List<SelectListItem> Genders { get; set; }

        [BindProperty]
        public int SelectedDivisionId { get; set; }

        [BindProperty]
        public int SelectedGender { get; set; }

        [BindProperty]
        public Worker Worker { get; set; }

        public EditModel(TestTaskContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Genders = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(v => new SelectListItem
                {
                    Value = ((int)v).ToString(),
                    Text = v.GetDescription()
                })
                .ToList();

            Divisions = _context.Division
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToList();

            if (id == 0)
            {
                Worker = new Worker();

                return Page();
            }

            Worker = _context.Worker
                .Include(w => w.Division).FirstOrDefault(m => m.Id == id);

            if (Worker is null)
                return NotFound();

            SelectedDivisionId = Worker.DivisionId;
            SelectedGender = Worker.GenderId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Worker.DivisionId = SelectedDivisionId;
            Worker.GenderId = SelectedGender;

            if (Worker.Id == 0)
            {
                _context.Worker.Add(Worker);
            }
            else
            {
                _context.Attach(Worker).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
