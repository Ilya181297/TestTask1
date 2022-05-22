#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public List<Division> Divisions { get; set; }
        public List<Worker> Workers { get; set; }
        public Division SelectedDivision { get; set; }

        public async Task OnGetAsync()
        {
            Divisions = _context.Division
                .ToList().FindAll(x => !x.ParentId.HasValue);
            Workers = await _context.Worker.Include(w => w.Division).ToListAsync();
        }

        public async Task<IActionResult> OnGetFilter(int id)
        {
            Divisions = _context.Division
                 .ToList().FindAll(x => !x.ParentId.HasValue);

            SelectedDivision = await _context.Division.FindAsync(id);

            if (SelectedDivision is null)
            {
                Workers = _context.Worker.Include(w => w.Division).ToList();

                return Page();
            }

            FillRecursive(SelectedDivision);

            Workers = _context.Worker
                .Where(x => _childIds.Contains(x.DivisionId) || x.DivisionId == id)
                .Include(x => x.Division)
                .ToList();

            return Page();
        }

        private readonly List<int> _childIds = new();
        private void FillRecursive(Division division)
        {
            foreach (var child in division.Children)
            {
                _childIds.Add(child.Id);
                FillRecursive(child);
            }
        }
    }
}
