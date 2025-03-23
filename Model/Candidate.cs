using static ResumeApp.Model.ResumeModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ResumeApp.Model
{
    public class Candidate
    {
        public int Id { get; set; }
        [Required]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;
        [Required, Validations.EmailValidationAttribute]
        [JsonPropertyName("email")] 
        public string Email { get; set; } = string.Empty;
        [RegularExpression(pattern:Validations.mobileRegex, ErrorMessage = "Mobile number must be 10 digits")]
        [JsonPropertyName("mobile")]
        public string? Mobile { get; set; }
        [JsonPropertyName("degreeId")]
        public int? DegreeId { get; set; }
        public byte[]? CV { get; set; }
        [JsonPropertyName("creationTime")]
        public DateTime CreationTime { get; set; }
    }
}
