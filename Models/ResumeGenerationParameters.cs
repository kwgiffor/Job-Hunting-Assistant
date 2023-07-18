using System.Text;

namespace JobHuntingAssistant.Models
{
    /// <summary>
    /// Parameters for the generation of a new resume.
    /// </summary>
    public class ResumeGenerationParameters
    {
        /// <summary>
        /// The job listing to generate a new resume for.
        /// </summary>
        public JobListing JobListing { get; set; }
        
        /// <summary>
        /// The user to generate a new resume for.
        /// </summary>
        public User User { get; set; }

        public ResumeGenerationParameters(JobListing jobListing, User user)
        {
            this.JobListing = jobListing;
            this.User = user;
        }

        /// <summary>
        /// Constructs a prompt for AI Models to use to generate a new resume.
        /// </summary>
        /// <returns></returns>
        public string ConstructPrompt()
        {
            StringBuilder prompt = new();

            // Start with the user's old resume
            prompt.AppendLine("Old Resume:");
            prompt.AppendLine(this.User.OldResume);

            // Add a separator
            prompt.AppendLine("\n---\n");

            // Add the job listing title and description
            prompt.AppendLine("New Job Listing:");
            prompt.AppendLine($"Job Title: {this.JobListing.Title}");
            prompt.AppendLine($"Job Description: {this.JobListing.Description}");

            // Add a separator
            prompt.AppendLine("\n---\n");

            // Ask GPT-4 to generate a new resume
            prompt.AppendLine("As a professional resume writer, please generate a new resume " 
                                + "using the information from the old resume, to highlight the User's " 
                                + "skills and experiences that are most relevant to the new job listing. " 
                                + "The new resume should be tailored to the job description provided and "
                                + "should fit on one page." 
                                + "Please optimize the new resume for keyword filtering, however try to refrain from copying phrases from the job description. "
                                + "Feel free to infer and add any skills and experiences that are not explicitly "
                                + "laid out in the supplied resume that would be required in any previous roles (Assume industry best practices were followed at all previous positions).");

            return prompt.ToString();
        }
    }
}