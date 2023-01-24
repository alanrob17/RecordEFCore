using System;
using System.Collections.Generic;

namespace RecordEFCore.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Name { get; set; }

    public string? Biography { get; set; }

    public virtual ICollection<Record> Records { get; } = new List<Record>();
}
