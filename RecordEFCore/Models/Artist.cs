using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecordEFCore.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string? FirstName { get; set; } = null;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Name { get; set; }

    public string? Biography { get; set; } = null;

    public virtual ICollection<Record> Records { get; } = new List<Record>();
}
