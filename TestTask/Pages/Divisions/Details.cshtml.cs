#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Divisions
{
    public class DetailsModel : PageModel
    {
        private readonly TestTaskContext _context;
        public Division Division { get; set; }

        public DetailsModel(TestTaskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Division = await _context.Division.Include(x => x.Parent).FirstOrDefaultAsync(m => m.Id == id);

            if (Division is null)
                return NotFound($"Division with Id={id} is not found");

            return Page();
        }
    }
}
