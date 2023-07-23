namespace JobHuntingAssistant.Models
{
    public class JobListing
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Resume> Resumes { get; set; }

        public JobListing()
        {
            Title= "";
            Description = "";
            Resumes = new List<Resume>();
        }

        public JobListing(string title, string description)
        {
            Title = title;
            Description = description;
            Resumes = new List<Resume>();
        }
    }
}