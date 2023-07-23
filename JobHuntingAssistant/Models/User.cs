namespace JobHuntingAssistant.Models
{
    public class User
    {
        public int Id { get; set; }
        public string OldResume { get; set; } = "";
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }

}