﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _dal = EFDAL.Data;
using EFDAL.Models;

namespace RecordEFCore.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Record>? Records { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var db = new _dal.RecordData();
            Records = db.GetRecordNames();
        }
    }
}