#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Divisions
{
    public class CreateModel : PageModel
    {
        private readonly TestTaskContext _context;
        public List<SelectListItem> Divisions { get; set; }

        [BindProperty]
        public int? SelectedDivisionId { get; set; }

        public CreateModel(TestTaskContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Divisions = _context.Division
                .Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.Name
                })
                .ToList();

            Divisions.Insert(0, new SelectListItem { Value = "0", Text = "Не выбрано"});
        }

        [BindProperty]
        public Division Division { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (SelectedDivisionId.HasValue && SelectedDivisionId > 0)
                Division.ParentId = SelectedDivisionId;

            _context.Division.Add(Division);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
