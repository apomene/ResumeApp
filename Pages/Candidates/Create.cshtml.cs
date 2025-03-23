using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ResumeApp.Model;
using static ResumeApp.Model.ResumeModel;

namespace ResumeApp.Pages.Candidate
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public ResumeApp.Model.Candidate Candidate { get; set; }

        public List<ResumeApp.Model.Degree> Degrees { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Degrees = await _db.Degrees.Distinct().ToListAsync();
            if (TempData["Candidate"] != null)
            {
                // Deserialize candidate data
                Candidate = JsonSerializer.Deserialize<ResumeApp.Model.Candidate>(TempData["Candidate"].ToString());
            }
            else
            {
                Candidate = new ResumeApp.Model.Candidate();
            }

            return Page();
        }
    }
}
