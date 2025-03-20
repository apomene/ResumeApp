﻿using System.ComponentModel.DataAnnotations;

namespace ResumeApp.Model
{
    public class ResumeModel
    {
        public class Candidate
        {
            public int Id { get; set; }
            [Required] public string LastName { get; set; } = string.Empty;
            [Required] public string FirstName { get; set; } = string.Empty;
            [Required, EmailAddress] public string Email { get; set; } = string.Empty;
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
    }
}
