#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly TestTask.Data.TestTaskContext _context;

        public List<SelectListItem> Divisions { get; set; }
        public List<SelectListItem> Genders { get; set; }

        [BindProperty]
        public int? SelectedDivisionId { get; set; }

        [BindProperty]
        public int SelectedGender { get; set; }
        public EditModel(TestTask.Data.TestTaskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Worker Worker { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Worker = await _context.Workers
                .Include(w => w.Division).FirstOrDefaultAsync(m => m.ID == id);

            if (Worker == null)
            {
                return NotFound();
            }
            Divisions = _context.Division
                .Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.Name
                })
            .ToList();

            Genders = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(v => new SelectListItem
                {
                    Value = ((int)v).ToString(),
                    Text = v.GetString()
                })
                .ToList();

            SelectedDivisionId = Worker.DivisionId;
            SelectedGender = Worker.Gender;

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

            Worker.DivisionId = (int)SelectedDivisionId;
            Worker.Gender = SelectedGender;
            _context.Attach(Worker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(Worker.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Index");
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.ID == id);
        }
    }
}
