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
        public enum Genders
        {
            Man = 0,
            Woman = 1
        }
        public IList<Division> Division { get; set; }
        public IList<Worker> Worker { get; set; }

        public async Task OnGetAsync()
        {
            Division = await _context.Division.ToListAsync();

            Worker = await _context.Workers
               .Include(w => w.Division).ToListAsync();
        }

        public async Task OnGetFilter(int? id)
        {
            Division = await _context.Division.ToListAsync();

            Worker = id == 0
                ? _context.Workers
                    .Include(w => w.Division).ToList()
                : _context.Workers
                    .Where(x => x.DivisionId == id)
                    .Include(w => w.Division).ToList();
        }

    }
}
