using System.ComponentModel.DataAnnotations;

namespace ResumeApp.Model
{
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
}
