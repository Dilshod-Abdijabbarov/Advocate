using Advocate.Dtos;
using Advocate.Models.Entity;

namespace Advocate.Interfaces
{
    public interface IFeedbackService
    {
        Task<bool> CreateFeedbackAsync(FeedbackDto feedback);
        Task<List<FeedbackDto>> GetAllFeedbacksAsync(int firs,int row);

    }
}
