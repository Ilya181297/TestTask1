﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Pages.Workers
{
    public class DetailsModel : PageModel
    {
        private readonly TestTask.Data.TestTaskContext _context;
        public DetailsModel(TestTask.Data.TestTaskContext context)
        {
            _context = context;
        }

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

            return Page();
        }
    }
}