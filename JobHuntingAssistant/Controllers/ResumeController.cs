using Microsoft.AspNetCore.Mvc;
using JobHuntingAssistant.Models;
using JobHuntingAssistant.Services;

namespace JobHuntingAssistant.Controllers
{
    /// <summary>
    /// Controller for handling the generation of resumes
    /// </summary>
    public class ResumeController : Controller
    {
        private readonly IResumeGenerationService _resumeGenerationService;
        private readonly IJobListingService _jobListingService;
        private readonly IUserService _userService;

        public ResumeController(IResumeGenerationService resumeGenerationService, IJobListingService jobListingService, IUserService userService)
        {
            _resumeGenerationService = resumeGenerationService;
            _jobListingService = jobListingService;
            _userService = userService;
        }

        /// <summary>
        /// Generates a resume from the given JobListing and User
        /// </summary>
        [HttpPost]
        public IActionResult GenerateResume(int jobListingId)
        {
            // Get the JobListing and User details
            JobListing jobListing = _jobListingService.GetJobListingById(jobListingId);
            User user = _userService.GetActiveUser();

            Console.WriteLine($" JobListing: {jobListing}, User: {user}");

            // Create the ResumeGenerationParameters object
            ResumeGenerationParameters parameters = new(jobListing, user);

            // Call the GenerateResume method in the IAIModel service
            Resume generatedResume = _resumeGenerationService.GenerateResume(parameters);

            // Return the generated resume
            return Json(generatedResume);
        }


        public IActionResult ViewResume(int id)
        {
            // TODO: Implement this
            return View();
        }
    }
}