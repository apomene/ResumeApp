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
        public class Candidate
        {
            public int Id { get; set; }
            [Required] public string LastName { get; set; } = string.Empty;
            [Required] public string FirstName { get; set; } = string.Empty;
            [Required, EmailValidationAttribute] public string Email { get; set; } = string.Empty;
            [RegularExpression("^\\d{10}$", ErrorMessage = "Mobile number must be 10 digits")] public string? Mobile { get; set; }
            public int? DegreeId { get; set; }
            public Degree? Degree { get; set; }
            public byte[]? CV { get; set; }
            public DateTime CreationTime { get; set; }
        }

        public class Degree
        {
            public int Id { get; set; }
            [Required] public string Name { get; set; } = string.Empty;
            public DateTime CreationTime { get; set; }
        }

        public enum Degrees
        {
            Bachelors,
            Masters,
            PhD
        }

        public class EmailValidationAttribute : ValidationAttribute
        {
            private static readonly Regex EmailRegex = new Regex(
                "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
                RegexOptions.Compiled);

            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                if (value is string email && EmailRegex.IsMatch(email))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Invalid email format");
            }
        }
    }
}
