using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ResumeApp.Model.ResumeModel;

namespace ResumeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DegreesController : ControllerBase
    {

        private readonly AppDbContext _db;
        public DegreesController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> CreateDegree(Degree degree)
        {
            if (string.IsNullOrWhiteSpace(degree.Name)) return BadRequest("Degree name is required");
            degree.CreationTime = DateTime.UtcNow;
            _db.Degrees.Add(degree);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDegree), new { id = degree.Id }, degree);
        }

        [HttpGet]
        public async Task<IActionResult> GetDegrees() => Ok(await _db.Degrees.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDegree(int id)
        {
            var degree = await _db.Degrees.FindAsync(id);
            return degree == null ? NotFound() : Ok(degree);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDegree(int id)
        {
            var degree = await _db.Degrees.FindAsync(id);
            if (degree == null) return NotFound();
            if (await _db.Candidates.AnyAsync(c => c.DegreeId == id)) return BadRequest("Degree is associated with candidates");
            _db.Degrees.Remove(degree);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
