using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ResumeApp.Model
{
    public class ResumeModel
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
            public DbSet<Candidate> Candidates => Set<Candidate>();
            public DbSet<Degree> Degrees => Set<Degree>();
        }
            
       
    }
}
