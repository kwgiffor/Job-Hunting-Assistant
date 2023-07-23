using JobHuntingAssistant.AI;
using JobHuntingAssistant.Models;

namespace JobHuntingAssistant.Services
{
    /// <summary>
    /// Service for generating a resume using an AI model
    /// </summary>
    public class AIResumeGenerationService : IResumeGenerationService
    {
        private readonly IAIModel _aiModel;

        public AIResumeGenerationService(IAIModel aiModel)
        {
            _aiModel = aiModel;
        }

        /// <summary>
        /// Generates a resume from the given parameters
        /// </summary>
        public Resume GenerateResume(ResumeGenerationParameters parameters)
        {
            // Construct the prompt from the parameters
            var resumePrompt = parameters.ConstructPrompt();

            // Get the generated text from the AI model ad parse it into a Resume object
            var unparsedString = _aiModel.Prompt(resumePrompt);
            var resumeResult = ParseGeneratedTextIntoResume(parameters, unparsedString);

            return resumeResult;
        }
        

        /// <summary>
        /// Parses the generated text into a Resume object
        /// </summary>
        private static Resume ParseGeneratedTextIntoResume(ResumeGenerationParameters parameters, string generatedText)
        {
            // Assuming the entire generated text is the content of the resume
            var content = generatedText;

            return new Resume(                
                parameters.User.Id,
                parameters.JobListing.Id,
                content
            );
        }
    }
}