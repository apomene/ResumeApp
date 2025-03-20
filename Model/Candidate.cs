using static ResumeApp.Model.ResumeModel;
using System.ComponentModel.DataAnnotations;

namespace ResumeApp.Model
{
    public class Candidate
    {
        public int Id { get; set; }
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required, Validations.EmailValidationAttribute] public string Email { get; set; } = string.Empty;
        [RegularExpression(pattern:Validations.mobileRegex, ErrorMessage = "Mobile number must be 10 digits")] public string? Mobile { get; set; }
        public int? DegreeId { get; set; }
        public Degree? Degree { get; set; }
        public byte[]? CV { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
