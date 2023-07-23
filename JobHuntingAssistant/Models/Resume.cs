namespace JobHuntingAssistant.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JobListingId { get; set; }
        public string Content { get; set; }
        public DateTime GeneratedAt { get; set; }

        public Resume(int userId, int jobListingId, string content)
        {
            UserId = userId;
            JobListingId = jobListingId;
            Content = content ?? string.Empty;
            GeneratedAt = DateTime.Now;
        }

        public Resume()
        {
            Content = string.Empty;
            GeneratedAt = DateTime.Now;
        }
    }
    
}