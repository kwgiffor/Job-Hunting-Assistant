using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JobHuntingAssistant.Models;
using JobHuntingAssistant.Services;
using Microsoft.AspNetCore.Authorization;

namespace JobHuntingAssistant.Controllers
{
    /// <summary>
    /// Controller for the home page.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJobListingService _jobListingService;

        public HomeController(ILogger<HomeController> logger, IJobListingService jobListingService)
        {
            _logger = logger;
            _jobListingService = jobListingService;
        }

        public IActionResult Index()
        {
            return View(new User());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult ResumeGeneration()
        {
            var model = new ResumeGenerationViewModel
            {
                JobListings = _jobListingService.GetAllJobListings() ?? new List<JobListing>(),
                SelectedJobListingIndex = 0
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}