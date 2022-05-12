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

namespace TestTask.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TestTaskContext _context;
        public IndexModel(TestTaskContext context)
        {
            _context = context;
        }
        public IList<Division> Division { get; set; }
        public IList<Worker> Worker { get; set; }

        public Division? SelectedDivision { get; set; }
        public async Task OnGetAsync()
        {
            Division = await _context.Division.ToListAsync();

            Worker = await _context.Workers
               .Include(w => w.Division).ToListAsync();
        }

        public async Task<IActionResult> OnGetFilter(int? id)
        {
            Division = await _context.Division.ToListAsync();

            var rootDivision = _context.Division.Find(id);
            SelectedDivision = rootDivision;
            if (rootDivision is null)
            {
                Worker = _context.Workers
                    .Include(w => w.Division)
                    .ToList();
                return Page();
            }

            FillRecursive(rootDivision);

            Worker = _context.Workers
                .Where(x => divisionIds.Contains(x.DivisionId) || x.DivisionId == id)
                .Include(x => x.Division)
                .ToList();

            return Page(); 
        }

        private List<int> divisionIds = new List<int>();
        private void FillRecursive(Division division)
        {
            foreach (var child in division.Children)
            {
                divisionIds.Add(child.ID);
                FillRecursive(child);
            }
        }
    }
}
