using Advocate.Dtos;
using Advocate.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advocate.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralService _generalService;
        public GeneralController(IGeneralService generalService)
        {
            _generalService = generalService;
        }

        [HttpPost]
        public async Task<bool> CreateFeedback([FromBody] FeedbackDto feedback)
            => await _generalService.CreateOrUpdateFeedbackAsync(feedback);
        [HttpGet]
        public async Task<List<FeedbackDto>> GetAllFeedbacks(int first, int row)
            => await _generalService.GetAllFeedbacksAsync(first, row);


        [HttpPost]
        public async Task<bool> CreateOrUpdateArticle([FromBody] ArticleDto articleDto)
            => await _generalService.CreateOrUpdateArticleAsync(articleDto);
        [HttpGet]
        public async Task<List<ArticleDto>> GetAllArticles(int first, int row)
            => await _generalService.GetAllArticlesAsync(first, row);


        [HttpPost]
        public async Task<bool> CreateOrUpdateWorkHistory([FromBody] WorkHistoryDto workHistoryDto)
            => await _generalService.CreateOrUpdateWorkHistoryAsync(workHistoryDto);
        [HttpGet]
        public async Task<List<WorkHistoryDto>> GetAllWorkHistories(int first, int row)
            => await _generalService.GetAllWorkHistoriesAsync(first, row);

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, Guid? imageGuid)
        {
            if (file != null && file.Length > 0)
            {
                if (!string.Equals(file.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                        !string.Equals(file.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                        !string.Equals(file.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                        !string.Equals(file.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
                        !string.Equals(file.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                        !string.Equals(file.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
                {
                    return Conflict();
                }

                return Ok(await _generalService.UploadImageAsync(file, imageGuid));
            }

            return NotFound();
        }
    }
}
