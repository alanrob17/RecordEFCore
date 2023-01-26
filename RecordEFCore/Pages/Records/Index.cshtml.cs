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
    public class IndexModel : PageModel
    {
        private readonly RecordDbContext _context;

        public IndexModel(RecordDbContext context)
        {
            _context = context;
        }

        public IList<Record> Record { get;set; } = default!;

        //public async Task OnGetAsync()
        //{
        //    if (_context.Records != null)
        //    {
        //        Record = await _context.Records
        //        .Include(@ => @.Artist).ToListAsync();
        //    }
        //}

        public async Task OnGetAsync()
        {
            if (_context != null)
            {
                Record = await _context.Records
                .Include(r => r.Artist).ToListAsync();
            }
        }
    }
}
