using Advocate.Dtos;
using Advocate.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advocate.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<bool> CreateFeedback([FromBody] FeedbackDto feedback)
            => await _feedbackService.CreateFeedbackAsync(feedback);

        [HttpGet]
        public async Task<List<FeedbackDto>> GetAllFeedbacksAsync(int firs, int row)
            => await _feedbackService.GetAllFeedbacksAsync(firs, row);
    }
}
