using EFDAL.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace EFDAL.Data
{
    public class ArtistData
    {
        /// <summary>
        /// Show a list of artist Names.
        /// </summary>
        public List<Artist>? GetArtistNames()
        {
            List<Artist> artists = new List<Artist>();

            using (var context = new RecordDbContext())
            {
                artists = context.Artists.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();
            }

            return artists;
        }
    }
}