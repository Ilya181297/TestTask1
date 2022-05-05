#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Divisions
{
    public class DeleteModel : PageModel
    {
        private readonly TestTask.Data.TestTaskContext _context;

        public DeleteModel(TestTask.Data.TestTaskContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Division = await _context.Division.FindAsync(id);

            if (Division != null)
            {
                _context.Division.Remove(Division);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
