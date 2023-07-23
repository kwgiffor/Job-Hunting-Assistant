using JobHuntingAssistant.Models;
namespace JobHuntingAssistant.Services
{
    /// <summary>
    /// In-memory implementation of <see cref="IJobListingService"/>.
    /// </summary>
    public class InMemoryJobListingService : IJobListingService
    {
        private static readonly List<JobListing> _jobListings = new();

        public InMemoryJobListingService()
        {
        }

        public List<JobListing> GetAllJobListings()
        {
            return _jobListings;
        }

        public JobListing GetJobListingById(int id)
        {
            var jobListing = _jobListings.FirstOrDefault(j => j.Id == id);
            return jobListing ?? throw new Exception($"Job listing with ID {id} not found.");
        }

        public void AddJobListing(JobListing jobListing)
        {
            jobListing.Id = _jobListings.Count;

            if (_jobListings.Any(j => j.Id == jobListing.Id))
            {
                throw new Exception($"Job listing with ID {jobListing.Id} already exists.");
            }
            _jobListings.Add(jobListing);
        }
        
        public void AddResumeToJobListing(int id, Resume resume)
        {
            var jobListing = GetJobListingById(id);

            jobListing.Resumes ??= new List<Resume>();

            if (jobListing.Resumes.Any(r => r.Id == resume.Id))
            {
                throw new Exception($"Resume with ID {resume.Id} already added to job listing {id}.");
            }
            
            jobListing.Resumes.Add(resume);
        }

    }
}