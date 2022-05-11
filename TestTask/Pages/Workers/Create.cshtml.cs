#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Workers
{
    public class CreateModel : PageModel
    {
        private readonly TestTask.Data.TestTaskContext _context;
        public List<SelectListItem> Divisions { get; set; }
        public List<SelectListItem> Genders { get; set; }
        public CreateModel(TestTask.Data.TestTaskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int SelectedDivisionId { get; set; }

        [BindProperty]
        public int SelectedGender { get; set; }

        public void OnGet()
        {
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
        }

        [BindProperty]
        public Worker Worker { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Worker.DivisionId = SelectedDivisionId;
            Worker.Gender = SelectedGender;

            _context.Workers.Add(Worker);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
