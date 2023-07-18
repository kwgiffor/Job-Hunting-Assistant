using JobHuntingAssistant.Models;

namespace JobHuntingAssistant.Services
{
    /// <summary>
    /// Interface for job listing services.
    /// </summary>
    public interface IJobListingService
    {
        /// <summary>
        /// Gets all job listings
        /// </summary>
        List<JobListing> GetAllJobListings();

        /// <summary>
        /// Gets a job listing by id
        /// </summary>
        /// <returns> The job listing, or null if not found</returns>
        JobListing GetJobListingById(int id);

        /// <summary>
        /// Adds a resume to a job listing.
        /// </summary>
        void AddResumeToJobListing(int id, Resume resume);

        /// <summary>
        /// Adds a job listing to the database.
        /// </summary>
        void AddJobListing(JobListing jobListing);
    }
}