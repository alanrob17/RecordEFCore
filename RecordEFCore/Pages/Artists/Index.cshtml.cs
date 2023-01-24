using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecordEFCore.Models;

namespace RecordEFCore.Pages.Artists
{
    public class IndexModel : PageModel
    {
        private readonly RecordDbContext _context;

        public IndexModel(RecordDbContext context)
        {
            _context = context;
        }

        public IList<Artist> Artist { get;set; } = default!;

        //public async Task OnGetAsync()
        //{
        //    if (_context.Artists != null)
        //    {
        //        Artist = await _context.Artists.ToListAsync();
        //    }
        //}

        public async Task OnGetAsync()
        {
            if (_context.Artists != null)
            {
                Artist = await _context.Artists.ToListAsync();

                foreach (var artist in Artist)
                {
                    if (!string.IsNullOrEmpty(artist.Biography))
                    {
                        artist.Biography = artist.Biography.Substring(0, Math.Min(30, artist.Biography.Length)) + "...";
                    }
                }
            }
        }
    }
}
