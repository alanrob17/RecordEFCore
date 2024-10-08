﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public async Task OnGetAsync()
        {
            if (_context.Artists != null)
            {
                Artist = await _context.Artists.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToListAsync();

                foreach (var artist in Artist)
                {
                    if (!string.IsNullOrEmpty(artist.Biography))
                    {
                        if (!string.IsNullOrEmpty(artist.Biography) && artist.Biography.Length > 80)
                        {
                            string text = artist.Biography;
                            text = Regex.Replace(text, "<.*?>", String.Empty);
                            text = string.Concat("<span>", text.AsSpan(0, 80), "&hellip;</span>");
                            artist.Biography = text;
                        }

                    }
                }
            }
        }
    }
}
