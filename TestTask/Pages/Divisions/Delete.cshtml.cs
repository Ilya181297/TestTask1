#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Divisions
{
    public class DeleteModel : PageModel
    {
        private readonly TestTaskContext _context;

        [BindProperty]
        public Division Division { get; set; }

        public DeleteModel(TestTaskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Division = await _context.Division.FindAsync(id);

            if (Division is null)
                return NotFound($"Division with Id={id} is not found");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Division = await _context.Division.FindAsync(id);

            if (Division is null)
                return NotFound($"Division with Id={id} is not found");

            _context.Division.Remove(Division);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
