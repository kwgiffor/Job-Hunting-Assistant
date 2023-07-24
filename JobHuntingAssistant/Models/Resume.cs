using System.ComponentModel.DataAnnotations;

namespace JobHuntingAssistant.Models
{
    public class Resume
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int JobListingId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime GeneratedAt { get; set; }

        public Resume()
        {
            Content = string.Empty;
            GeneratedAt = DateTime.Now;
        }
    }
    
}