using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ResumeApp.Model;

namespace ResumeApp.Pages.Degree
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ResumeApp.Model.Degree Degree { get; set; }

        public IActionResult OnGet()
        {
            if (TempData["Degree"] != null)
            {               
                Degree = JsonSerializer.Deserialize<ResumeApp.Model.Degree>(TempData["Degree"].ToString());
            }
            else
            {
                Degree = new ResumeApp.Model.Degree();
            }

            return Page();
        }
    }
}
