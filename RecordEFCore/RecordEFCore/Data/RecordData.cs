using RecordEFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordEFCore.Data
{
    public class RecordData
    {
        /// <summary>
        /// Show a list of Record Names.
        /// </summary>
        public List<Record>? GetRecordNames()
        {
            List<Record> records = new List<Record>();

            using (var context = new RecordDbContext())
            {
                records = context.Records.OrderBy(r => r.RecordId).ThenBy(r => r.Recorded).ToList();
            }

            return records;
        }
    }
}
