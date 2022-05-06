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
    public class IndexModel : PageModel
    {
        private readonly TestTask.Data.TestTaskContext _context;

        public IndexModel(TestTask.Data.TestTaskContext context)
        {
            _context = context;
        }

        public IList<Division> Division { get;set; }

        public async Task OnGetAsync()
        {
            Division = await _context.Division.ToListAsync();
        }

    }
}
