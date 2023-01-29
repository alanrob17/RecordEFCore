using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecordEFCore.Models;

namespace RecordEFCore.Pages.Artists
{
    public class CreateModel : PageModel
    {
        private readonly RecordDbContext _context;

        public CreateModel(RecordDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Artist Artist { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Artists == null || Artist == null)
            {
                return Page();
            }

            Artist.Name = string.IsNullOrEmpty(Artist.FirstName) ? Artist.LastName : $"{Artist.FirstName} {Artist.LastName}";

            _context.Artists.Add(Artist);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
