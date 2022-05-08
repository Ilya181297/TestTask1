﻿#nullable disable
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
            Divisions = _context.Division
            .Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.Name
            })
            .ToList();

            if (id == null)
            {
                return NotFound();
            }

            Division = await _context.Division.Include(x => x.Parent).FirstOrDefaultAsync(m => m.ID == id);

            if (Division == null)
            {
                return NotFound();
            }
         
            Divisions.Insert(0, new SelectListItem { Value = "0", Text = "Корневой" });
               // if (Division.ID.Equals(Divisions))
                Divisions.Remove(Divisions.SingleOrDefault(r => r.Value == Division.ID.ToString()));

            if (SelectedDivisionId.HasValue && SelectedDivisionId > 0)
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

            _context.Attach(Division).State = EntityState.Modified;

            if (SelectedDivisionId.HasValue && SelectedDivisionId > 0)
                Division.ParentId = SelectedDivisionId;

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
