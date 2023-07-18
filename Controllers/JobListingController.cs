using Microsoft.AspNetCore.Mvc;
using JobHuntingAssistant.Services;
using JobHuntingAssistant.Models;

namespace JobHuntingAssistant.Controllers
{
    /// <summary>
    /// Controller for Job Listing related actions.
    /// </summary>
    public class JobListingController : Controller
    {
        private readonly IJobListingService _jobListingService;
        private readonly ILogger<JobListingController> _logger;

        public JobListingController(IJobListingService jobListingService, ILogger<JobListingController> logger)
        {
            _jobListingService = jobListingService;
            _logger = logger;
        }

        /// <summary>
        /// Action for adding a resume to a job listing
        /// </summary>
        [HttpPost]
        public IActionResult AddResume(int id, Resume resume)
        {
            if (resume == null)
            {
                return BadRequest();
            }
            try
            {
                _jobListingService.AddResumeToJobListing(id, resume);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return NotFound();
            }
            return RedirectToAction("Details", new { id });
        }

        /// <summary>
        /// Action for adding a new job listing.
        /// </summary>
        [HttpPost]
        public IActionResult AddJobListing(JobListing jobListing)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }

            _jobListingService.AddJobListing(jobListing);
            return Json(new { success = true, jobListing });
        }
        
        /// <summary>
        /// Action for getting a job listing by id.
        /// </summary>
        public IActionResult GetJobListing(int id)
        {
            var jobListing = _jobListingService.GetJobListingById(id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return PartialView("_JobListingDetails", jobListing);
        }
    }
}