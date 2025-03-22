using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ResumeApp.Model.ResumeModel;
using System;
using Microsoft.EntityFrameworkCore;

namespace ResumeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : Controller
    {
        private readonly AppDbContext _db;
        public CandidatesController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> CreateCandidate(Model.Candidate candidate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            candidate.CreationTime = DateTime.UtcNow;
            _db.Candidates.Add(candidate);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCandidate), new { id = candidate.Id }, candidate);
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidates() => Ok(await _db.Candidates.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidate(int id)
        {
            var candidate = await _db.Candidates.FindAsync(id);
            return candidate == null ? NotFound() : Ok(candidate);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Create(int id)
        {
            var candidate = await _db.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return RedirectToPage("/Candidates/Edit");
            }

            TempData["Candidate"] = System.Text.Json.JsonSerializer.Serialize(candidate);

            return RedirectToPage("/Candidates/Edit");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await _db.Candidates.FindAsync(id);
            if (candidate == null) return NotFound();
            _db.Candidates.Remove(candidate);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCandidate(int id, Model.Candidate updatedCandidate)
        {

            // Check if the updatedCandidate is valid
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Check if the candidate exists
            var existingCandidate = await _db.Candidates.FindAsync(id);
            if (existingCandidate == null)
            {
                return NotFound(new { message = $"Candidate with ID {id} not found." });
            }

            // Update fields 
            existingCandidate.FirstName = updatedCandidate.FirstName;
            existingCandidate.LastName = updatedCandidate.LastName;
            existingCandidate.Email = updatedCandidate.Email;
            existingCandidate.Mobile = updatedCandidate.Mobile;
            existingCandidate.DegreeId = updatedCandidate.DegreeId;
            existingCandidate.CV = updatedCandidate.CV; // If applicable

            _db.Entry(existingCandidate).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(existingCandidate);
        }
    }
}

