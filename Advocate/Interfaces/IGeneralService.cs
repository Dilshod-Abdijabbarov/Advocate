using Advocate.Dtos;
using Advocate.Models.Entity;
using Advocate.Models.enums;

namespace Advocate.Interfaces
{
    public interface IGeneralService
    {
        Task<bool> CreateOrUpdateFeedbackAsync(FeedbackDto feedback);
        Task<ResponeMode<FeedbackDto>> GetAllFeedbacksAsync(int firs,int row, bool isAdmin);

        Task<bool> CreateOrUpdateArticleAsync(ArticleDto articleDto);
        Task<ResponeMode<ArticleDto>> GetAllArticlesAsync(int firs, int row, CaseType? caseType);

        Task<bool> CreateOrUpdateWorkHistoryAsync(WorkHistoryDto workHistoryDto);
        Task<ResponeMode<WorkHistoryDto>> GetAllWorkHistoriesAsync(int firs, int row);

        Task<Guid> UploadImageAsync(IFormFile file, Guid? imageGuid);
        Task<(byte[],string contentType)> GetImageByte(Guid id);
        Task<bool> ActiveFeedbackAsync(int id, bool isActive);
        Task<bool> LoginAsync(string username, string password);

        Task<Guid> UploadVideo(IFormFile file, Guid? videoGuid);
        Task<byte[]> GetVideoById(Guid id);
    }
}
