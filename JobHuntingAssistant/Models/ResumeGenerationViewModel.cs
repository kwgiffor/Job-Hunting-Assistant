using System.ComponentModel.DataAnnotations;

namespace JobHuntingAssistant.Models
{
   
   /// <summary>
   /// This class is used to store the data for the Resume Generation View
   /// </summary>
    public class ResumeGenerationViewModel
    {
        /// <summary>
        /// The list of job listings
        /// </summary>
        [Display(Name = "Job Listings")]
        public List<JobListing> JobListings { get; set; }

        /// <summary>
        /// The index of the selected job listing
        /// </summary>
        public int SelectedJobListingIndex { get; set; }

        public ResumeGenerationViewModel()
        {
            JobListings = new List<JobListing>();
            SelectedJobListingIndex = 0;
        }
    }

}