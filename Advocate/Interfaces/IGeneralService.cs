using Advocate.Dtos;
using Advocate.Models.Entity;

namespace Advocate.Interfaces
{
    public interface IGeneralService
    {
        Task<bool> CreateOrUpdateFeedbackAsync(FeedbackDto feedback);
        Task<ResponeMode<FeedbackDto>> GetAllFeedbacksAsync(int firs,int row, bool isAdmin);

        Task<bool> CreateOrUpdateArticleAsync(ArticleDto articleDto);
        Task<ResponeMode<ArticleDto>> GetAllArticlesAsync(int firs, int row);

        Task<bool> CreateOrUpdateWorkHistoryAsync(WorkHistoryDto workHistoryDto);
        Task<ResponeMode<WorkHistoryDto>> GetAllWorkHistoriesAsync(int firs, int row);

        Task<Guid> UploadImageAsync(IFormFile file, Guid? imageGuid);
        Task<bool> ActiveFeedbackAsync(int id);
    }
}
