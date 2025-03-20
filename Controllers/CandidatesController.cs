using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ResumeApp.Model.ResumeModel;
using System;
using Microsoft.EntityFrameworkCore;

namespace ResumeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
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
            throw new NotImplementedException();
        }
    }
}

