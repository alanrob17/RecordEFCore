using System;   
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecordEFCore.Models;

namespace RecordEFCore.Pages.Records
{
    public class DetailsModel : PageModel
    {
        private readonly RecordDbContext _context;

        public DetailsModel(RecordDbContext context)
        {
            _context = context;
        }

      public Record Record { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var record = await _context.Records.FirstOrDefaultAsync(m => m.RecordId == id);

            if (record == null)
            {
                return NotFound();
            }
            else 
            {
                var artist = await _context.Artists.FirstOrDefaultAsync(m => m.ArtistId == record.ArtistId);
                record.Artist.Name = artist.Name;
                Record = record;
            }
            return Page();
        }
    }
}
