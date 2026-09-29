using Advocate.Dtos;
using Advocate.Interfaces;
using Advocate.Models.enums;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<bool> CreateOrUpdateFeedback([FromBody] FeedbackDto feedback)
            => await _generalService.CreateOrUpdateFeedbackAsync(feedback);

        [HttpGet]
        public async Task<ResponeMode<FeedbackDto>> GetAllFeedbacks(int first, int row, bool isAdmin)
            => await _generalService.GetAllFeedbacksAsync(first, row,isAdmin);


        [HttpPost]
        public async Task<bool> CreateOrUpdateArticle([FromBody] ArticleDto articleDto)
            => await _generalService.CreateOrUpdateArticleAsync(articleDto);
        [HttpGet]
        public async Task<ResponeMode<ArticleDto>> GetAllArticles(int first, int row, CaseType? caseType)
            => await _generalService.GetAllArticlesAsync(first, row,caseType);


        [HttpPost]
        public async Task<bool> CreateOrUpdateWorkHistory([FromBody] WorkHistoryDto workHistoryDto)
            => await _generalService.CreateOrUpdateWorkHistoryAsync(workHistoryDto);


        [HttpGet]
        public async Task<ResponeMode<WorkHistoryDto>> GetAllWorkHistories(int first, int row)
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageFile(Guid id)
        {
            var (data,contentType) = await _generalService.GetImageByte(id).ConfigureAwait(false);
            return File(data, contentType, id.ToString());
        }

        [HttpPost]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public async Task<IActionResult> UploadVideo(Guid? id)
        {
            if (!Request.HasFormContentType)
                return BadRequest();
            var form = Request.Form;

            foreach (var file in form.Files)
            {
                if (file != null && file.Length > 0)
                {
                    return Ok(await _generalService.UploadVideo(file,id));
                }
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<byte[]> GetVideoById(Guid id)
        {
            return await _generalService.GetVideoById(id).ConfigureAwait(false);
        }

        [HttpPut]
        public async Task<bool> ActiveFeedback(int id, bool isActive)
            => await _generalService.ActiveFeedbackAsync(id, isActive);

        [HttpPost]
        public async Task<bool> Login(string username, string password)
           => await _generalService.LoginAsync(username, password);
        
    }
}
