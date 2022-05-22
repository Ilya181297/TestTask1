#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Divisions
{
    public class EditModel : PageModel
    {
        private readonly TestTaskContext _context;
        public List<SelectListItem> Divisions { get; set; }

        [BindProperty]
        public Division Division { get; set; }

        [BindProperty]
        public int SelectedParentId { get; set; }

        public EditModel(TestTaskContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(int id)
        {
            Divisions = _context.Division
                     .Where(x => x.Id != id)
                     .Select(x => new SelectListItem
                     {
                         Value = x.Id.ToString(),
                         Text = x.Name
                     })
                     .ToList();

            Divisions.Insert(0, new SelectListItem { Value = "0", Text = "Корневой" });

            if (id == 0)
            {
                Division = new Division();
                Division.FormationDate = DateTime.Today;

                return Page();
            }

            Division = _context.Division.FirstOrDefault(m => m.Id == id);
            if (Division is null)
                return NotFound();

            SelectedParentId = Division.ParentId ?? 0;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Division.Id == 0)
            {
                if (SelectedParentId > 0)
                    Division.ParentId = SelectedParentId;

                _context.Division.Add(Division);
            }
            else
            {
                Division.ParentId = SelectedParentId == 0 ? null : SelectedParentId;
                _context.Attach(Division).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}

