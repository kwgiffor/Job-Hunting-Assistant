using System.ComponentModel.DataAnnotations;

namespace JobHuntingAssistant.Models
{
    public class JobListing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Resume> Resumes { get; set; }

        public JobListing()
        {
            Title= "";
            Description = "";
            Resumes = new List<Resume>();
        }
    }
}