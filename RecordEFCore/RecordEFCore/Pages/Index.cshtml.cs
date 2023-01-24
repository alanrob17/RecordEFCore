using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordEFCore.Data;
using RecordEFCore.Models;

namespace RecordEFCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RecordDbContext context;

        public IndexModel(RecordDbContext _context)
        {
            context = _context;
        }

        [BindProperty]
        public List<ArtistRecord> ArtistRecords { get; set; }

        public void OnGet()
        {

            // Records = context.Records.OrderBy(r => r.RecordId).ThenBy(r => r.Recorded).ToList();
            var records = context.Records.Join(context.Artists, record => record.ArtistId, artist => artist.ArtistId, (record, artist) => new { record, artist }).ToList();

            var list = new List<ArtistRecord>();

            foreach (var ar in records)
            {
                ArtistRecord artist = new()
                {
                    Artist = ar.artist.Name,
                    Name = ar.record.Name,
                    Recorded = ar.record.Recorded,
                    Field = ar.record.Field,
                    Media = ar.record.Media,
                    Rating = ar.record.Rating
                };

                list.Add(artist);
            }

            ArtistRecords = list;
        }
    }

    public class ArtistRecord
    {
        public string? Artist { get; set; }
        public string? Name { get; set; }
        public int Recorded { get; set; }
        public string? Field { get; set; }
        public string? Media { get; set; }
        public string? Rating { get; set; }
    }
}