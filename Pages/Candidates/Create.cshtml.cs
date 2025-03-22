using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ResumeApp.Model;

namespace ResumeApp.Pages.Candidate
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ResumeApp.Model.Candidate Candidate { get; set; }

        public IActionResult OnGet()
        {
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
