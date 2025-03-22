using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ResumeApp.Model
{
    public class Degree
    {
        public int Id { get; set; }
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("creationTime")]
        public DateTime CreationTime { get; set; }
    }

    public enum Degrees
    {
        Bachelors,
        Masters,
        PhD
    }
}
