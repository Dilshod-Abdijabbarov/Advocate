using Advocate.Context;
using Advocate.Dtos;
using Advocate.Interfaces;
using Advocate.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Advocate.Services;

public class GeneralService : IGeneralService
{
    public readonly AdvocateDbContext _context;
    private readonly IConfiguration _configuration;
    public GeneralService(AdvocateDbContext context,IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<bool> CreateOrUpdateArticleAsync(ArticleDto articleDto)
    {
        var article = await _context.Articles.FindAsync(articleDto.Id);

        if (article == null)
        {
            article = new Article
            {
                Active = true,
                CreatedDate = DateTime.UtcNow.AddHours(5),
            };

            await _context.Articles.AddAsync(article);
        }

        article.Title = articleDto.Title;
        article.Active = articleDto.Active;
        article.CaseType = articleDto.CaseType;
        article.ImageUrl = articleDto.ImageUrl;
        article.Discription = articleDto.Discription;
        article.DetailedDiscription = articleDto.DetailedDiscription;
       
        if (await _context.SaveChangesAsync() > 0)
            return true;

        return false;
    }

    public async Task<bool> CreateOrUpdateFeedbackAsync(FeedbackDto feedbackDto)
    {
        var feedback = await _context.Feedbacks.FindAsync(feedbackDto.Id);

        if (feedback == null)
        {
            feedback = new Feedback
            {
                Active = true,
                CreatedDate = DateTime.UtcNow.AddHours(5)
            };

            await _context.Feedbacks.AddAsync(feedback);
        }

        feedback.Title = feedbackDto.Title;
        feedback.Active = feedbackDto.Active;
        feedback.LastName = feedbackDto.LastName;
        feedback.CaseType = feedbackDto.CaseType;
        feedback.FirstName = feedbackDto.FirstName;
        feedback.Discription = feedbackDto.Discription;      

        if (await _context.SaveChangesAsync() > 0)
            return true;

        return false;
    }

    public async Task<bool> CreateOrUpdateWorkHistoryAsync(WorkHistoryDto workHistoryDto)
    {
        var workHistory = await _context.WorkHistories.FindAsync(workHistoryDto.Id);

        if (workHistory == null)
        {
            workHistory = new WorkHistory
            {
                CreatedDate = DateTime.UtcNow.AddHours(5)
            };

            await _context.WorkHistories.AddAsync(workHistory);
        }

        workHistory.Title = workHistoryDto.Title;
        workHistory.Active = workHistoryDto.Active;
        workHistory.EndDate = workHistoryDto.EndDate;
        workHistory.ImageUrl = workHistoryDto.ImageUrl;
        workHistory.CaseType = workHistoryDto.CaseType;
        workHistory.StartDate = workHistoryDto.StartDate;
        workHistory.WorkStatus = workHistoryDto.WorkStatus;
        workHistory.Discription = workHistoryDto.Discription;
        workHistory.DetailedDiscription = workHistoryDto.DetailedDiscription;

        if (await _context.SaveChangesAsync() > 0)
            return true;

        return false;
    }

    public async Task<List<ArticleDto>> GetAllArticlesAsync(int firs, int row)
    {
        return await _context.Articles
            .Where(x => x.Active)
            .Skip(firs).Take(row)
            .Select(x => new ArticleDto
            {
                Id = x.Id,
                Title = x.Title,
                Active = x.Active,
                CaseType = x.CaseType,
                ImageUrl = x.ImageUrl,
                Discription = x.Discription,
                DetailedDiscription = x.DetailedDiscription,
            }).ToListAsync();
    }

    public async Task<List<FeedbackDto>> GetAllFeedbacksAsync(int firs, int row)
    {
        var feedbacks = await _context.Feedbacks
            .Where(x => x.Active)
            .Skip(firs).Take(row)
            .Select(x=> new FeedbackDto
            {
                Id = x.Id,
                Title = x.Title,
                LastName = x.LastName,
                CaseType = x.CaseType,
                FirstName = x.FirstName,
                Discription = x.Discription,
            }).ToListAsync();

        return feedbacks;
    }

    public async Task<List<WorkHistoryDto>> GetAllWorkHistoriesAsync(int firs, int row)
    {
        return await _context.WorkHistories
            .Where(x => x.Active)
            .Skip(firs).Take(row)
            .Select(x => new WorkHistoryDto
            {
                Id = x.Id,
                Title = x.Title,
                Active = x.Active,
                EndDate = x.EndDate,
                CaseType = x.CaseType,
                ImageUrl = x.ImageUrl,
                StartDate = x.StartDate,
                WorkStatus = x.WorkStatus,
                Discription = x.Discription,
                DetailedDiscription = x.DetailedDiscription,
            }).ToListAsync();
    }


    public async Task<Guid> UploadImageAsync(IFormFile file,Guid? imageGuid)
    {
#if DEBUG
                string path = Directory.GetCurrentDirectory() + _configuration["Files:PathImage"];
#else
                string path = _configuration["Files:PathImage"];
#endif

        if (!Directory.Exists(path))      
            Directory.CreateDirectory(path);

        if(imageGuid == null)
            imageGuid = Guid.NewGuid();

        path = Path.Combine(path, imageGuid.ToString());

        if (File.Exists(path))
            File.Delete(path);

        await using Stream fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return imageGuid??Guid.Empty;
    }
}
