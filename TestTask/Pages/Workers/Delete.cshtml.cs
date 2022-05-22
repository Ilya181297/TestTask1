#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Workers
{
    public class DeleteModel : PageModel
    {
        private readonly TestTaskContext _context;

        public DeleteModel(TestTaskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Worker Worker { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Worker = await _context.Worker
                .Include(w => w.Division).FirstOrDefaultAsync(m => m.Id == id);

            if (Worker is null)
                return NotFound($"Division with Id={id} is not found");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Worker = await _context.Worker.FindAsync(id);

            if (Worker is not null)
            {
                _context.Worker.Remove(Worker);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Index");
        }
    }
}
