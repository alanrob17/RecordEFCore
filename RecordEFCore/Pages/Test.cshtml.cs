using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RecordEFCore.Pages
{
    public class TestModel : PageModel
    {
        private readonly RecordDbContext _context;

        public TestModel(RecordDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<ArtistRecord> ArtistRecords { get; set; }

        public async Task OnGetAsync(int? artistId)
        {
            var records = await _context.Records
                .Join(_context.Artists, record => record.ArtistId, artist => artist.ArtistId, (record, artist) => new { record, artist })
                .Where(r => r.artist.ArtistId == artistId || artistId == null)
                .OrderBy(r => r.artist.LastName)
                .ThenBy(r => r.artist.FirstName)
                .ThenBy(r => r.record.Recorded)
                .ToListAsync();

            var list = new List<ArtistRecord>();

            foreach (var ar in records)
            {
                var artist = new ArtistRecord
                {
                    ArtistId = ar.artist.ArtistId,
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
        public int ArtistId { get; set; }
        public string? Artist { get; set; }
        public string? Name { get; set; }
        public int Recorded { get; set; }
        public string? Field { get; set; }
        public string? Media { get; set; }
        public string? Rating { get; set; }
    }
}