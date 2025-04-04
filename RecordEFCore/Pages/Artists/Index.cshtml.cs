using System;
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

        public PaginatedList<Artist> Artist { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 15;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public async Task OnGetAsync(int? pageIndex)
        {
            if (_context.Artists != null)
            {
                CurrentPage = pageIndex ?? 1;

                var artistQuery = _context.Artists.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).AsQueryable();

                Count = await artistQuery.CountAsync();
                Artist = await PaginatedList<Artist>.CreateAsync(artistQuery, CurrentPage, PageSize);

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
