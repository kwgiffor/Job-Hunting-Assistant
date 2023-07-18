using JobHuntingAssistant.Models;
namespace JobHuntingAssistant.Services
{
    /// <summary>
    /// Resume generation service interface.
    /// </summary>
    public interface IResumeGenerationService
    {
        /// <summary>
        /// Generates a resume from the given parameters.
        /// </summary>
        Resume GenerateResume(ResumeGenerationParameters parameters);
    }
}