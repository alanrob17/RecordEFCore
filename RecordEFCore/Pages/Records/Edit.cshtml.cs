﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecordEFCore.Models;

namespace RecordEFCore.Pages.Records
{
    public class EditModel : PageModel
    {
        private readonly RecordDbContext _context;

        public EditModel(RecordDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Record Record { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var record =  await _context.Records.FirstOrDefaultAsync(m => m.RecordId == id);
            if (record == null)
            {
                return NotFound();
            }
            Record = record;
           ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "LastName");
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

            _context.Attach(Record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(Record.RecordId))
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

        private bool RecordExists(int id)
        {
          return (_context.Records?.Any(e => e.RecordId == id)).GetValueOrDefault();
        }
    }
}
