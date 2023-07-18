namespace JobHuntingAssistant.Models
{
    public class User
    {
        public int Id { get; set; }
        public string OldResume { get; set; } = "";
        public ICollection<Resume> Resumes { get; set; } = new List<Resume>();
    }
}