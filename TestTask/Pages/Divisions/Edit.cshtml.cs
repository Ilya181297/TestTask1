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
        private readonly TestTask.Data.TestTaskContext _context;
        public List<SelectListItem> Divisions { get; set; }

        [BindProperty]
        public int? SelectedDivisionId { get; set; }

        public EditModel(TestTaskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Division Division { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Division = await _context.Division.FirstOrDefaultAsync(m => m.ID == id);

            if (Division == null)
            {
                return NotFound();
            }

            Divisions = _context.Division
                .Where(x => x.ID != id)
                .Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.Name
                })
            .ToList();

            Divisions.Insert(0, new SelectListItem { Value = "0", Text = "Корневой" });

            SelectedDivisionId = Division.ParentId;

            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Division.ParentId = !SelectedDivisionId.HasValue || SelectedDivisionId == 0 ? 
                null : SelectedDivisionId;

            _context.Attach(Division).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DivisionExists(Division.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DivisionExists(int id)
        {
            return _context.Division.Any(e => e.ID == id);
        }
    }
}
