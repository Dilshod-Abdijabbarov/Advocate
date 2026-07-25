using Advocate.Dtos;
using Advocate.Models.Entity;

namespace Advocate.Interfaces
{
    public interface IGeneralService
    {
        Task<bool> CreateOrUpdateFeedbackAsync(FeedbackDto feedback);
        Task<List<FeedbackDto>> GetAllFeedbacksAsync(int firs,int row);

        Task<bool> CreateOrUpdateArticleAsync(ArticleDto articleDto);
        Task<List<ArticleDto>> GetAllArticlesAsync(int firs, int row);

        Task<bool> CreateOrUpdateWorkHistoryAsync(WorkHistoryDto workHistoryDto);
        Task<List<WorkHistoryDto>> GetAllWorkHistoriesAsync(int firs, int row);

        Task<Guid> UploadImageAsync(IFormFile file, Guid? imageGuid);
    }
}
