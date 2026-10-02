using Advocate.Context;
using Advocate.Interfaces;
using Advocate.Models.Dtos;
using Advocate.Models.Entity;
using Advocate.Models.enums;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace Advocate.Services;

public class GeneralService : IGeneralService
{
    public readonly AdvocateDbContext _context;
    private readonly IConfiguration _configuration;
    public GeneralService(AdvocateDbContext context, IConfiguration configuration)
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
                DetailedDiscription = "",   
            };

            await _context.Articles.AddAsync(article);
        }

        article.Title = articleDto.Title;
        article.Active = articleDto.Active;
        article.CaseType = articleDto.CaseType;
        article.ImageUrl = articleDto.ImageUrl;
        article.Discription = articleDto.Discription;
        article.CreatedDate = articleDto.CreatedDate;

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
                Active = false,
                CreatedDate = DateTime.UtcNow.AddHours(5)
            };

            await _context.Feedbacks.AddAsync(feedback);
        }

        feedback.Title = feedbackDto.Title;
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

    public async Task<ResponeMode<ArticleDto>> GetAllArticlesAsync(int firs, int row,CaseType? caseType)
    {
        var query = _context.Articles
            .Where(x => x.Active).AsNoTracking();

        if(caseType != null)
            query = query.Where(x=>x.CaseType == caseType);

        var result = new ResponeMode<ArticleDto>
        {
            TotalItems = await query.CountAsync(),
            Items = await query
                .Skip(firs).Take(row)
                .Select(x => new ArticleDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Active = x.Active,
                    CaseType = x.CaseType,
                    ImageUrl = x.ImageUrl,
                    Discription = x.Discription,
                }).ToListAsync()
        };

        return result;
    }

    public async Task<ResponeMode<FeedbackDto>> GetAllFeedbacksAsync(int firs, int row, bool isAdmin)
    {
        var query = _context.Feedbacks.AsNoTracking();

        if (!isAdmin)
            query = query.Where(x => x.Active);

        var result = new ResponeMode<FeedbackDto>
        {
            TotalItems = await query.CountAsync(),
            Items = await query
                .Skip(firs).Take(row)
                .Select(x => new FeedbackDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Active = x.Active,
                    LastName = x.LastName,
                    CaseType = x.CaseType,
                    FirstName = x.FirstName,
                    Discription = x.Discription,
                }).ToListAsync()
        };

        return result;
    }

    public async Task<ResponeMode<WorkHistoryDto>> GetAllWorkHistoriesAsync(int firs, int row)
    {
        var query = _context.WorkHistories
            .Where(x => x.Active).AsNoTracking();

        var result = new ResponeMode<WorkHistoryDto>
        {
            TotalItems = await query.CountAsync(),
            Items = await query
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
                }).ToListAsync()
        };

        return result;
    }


    public async Task<Guid> UploadImageAsync(IFormFile file, Guid? imageGuid)
    {
#if DEBUG
        string path = Directory.GetCurrentDirectory() + _configuration["Files:PathImage"];
#else
                string path = _configuration["Files:PathImage"];
#endif

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        if (imageGuid == null)
            imageGuid = Guid.NewGuid();

        path = Path.Combine(path, imageGuid.ToString());

        if (File.Exists(path))
            File.Delete(path);

        await using Stream fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return imageGuid ?? Guid.Empty;
    }


    public async Task<(byte[],string)> GetImageByte(Guid id)
    {
#if DEBUG

        string path = Directory.GetCurrentDirectory() + _configuration["Files:PathImage"];
#else
             string path = _configuration["Files:PathImage"];   
#endif
        string fileFullPath = Path.Combine(path, id.ToString());

        if (System.IO.File.Exists(fileFullPath))
        {
            var buffer = await System.IO.File.ReadAllBytesAsync(fileFullPath);
            var contentType = GetMimeTypeFromBytes(buffer);
            return (buffer, contentType);
        }

        return (Array.Empty<byte>(),null);
    }

    private string GetMimeTypeFromBytes(byte[] bytes)
    {
        if (bytes == null || bytes.Length < 4) return "application/octet-stream";
        if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF) return "image/jpeg";
        if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47) return "image/png";
        if (bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46) return "image/gif";
        if (bytes[0] == 0x52 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x46) return "image/webp";
        return "application/octet-stream";
    }

    public async Task<Guid> UploadVideo(IFormFile file,Guid? videoGuid)
    {
#if DEBUG

        string path = Directory.GetCurrentDirectory() + _configuration["Files:PathVideo"];
#else
             string path = _configuration["Files:PathVideo"];   
#endif

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        if (videoGuid == null)
            videoGuid = Guid.NewGuid();

        path = Path.Combine(path, videoGuid.ToString());
        await using Stream fileStream = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(fileStream);
        return videoGuid ?? Guid.Empty;
    }

    public async Task<byte[]> GetVideoById(Guid id)
    {
#if DEBUG

            string path = Directory.GetCurrentDirectory() + _configuration["Files:PathVideo"];
#else
             string path = _configuration["Files:PathVideo"];   
#endif

            string fileFullPath = Path.Combine(path, id.ToString());

            if (File.Exists(fileFullPath))
            {
                var buffer = await File.ReadAllBytesAsync(fileFullPath);
                //return Convert.ToBase64String(buffer);
                return buffer;
            }
        
        return Array.Empty<byte>();
    }

    public async Task<bool> CreateOrUpdateContentAsync(ContentDto contentDto)
    {
        var content = await _context.Contents.FindAsync(contentDto.Id);

        if (content == null)
        {
            content = new Content
            {
                CreatedDate = DateTime.UtcNow.AddHours(5)
            };

            await _context.Contents.AddAsync(content);
        }

        content.Title = contentDto.Title;
        content.VideoUrl = contentDto.VideoUrl;
        content.CaseType = contentDto.CaseType;
        content.Description = contentDto.Description;
        content.ImageUrl = contentDto.ImageUrl;
        content.ContentType = contentDto.ContentType;

        if (await _context.SaveChangesAsync() > 0)
            return true;

        return false;
    }

    public async Task<ResponeMode<ContentDto>> GetAllContentAsync(int firs, int row,Models.enums.ContentType contentType)
    {
        var query = _context.Contents.Where(x=>x.ContentType == contentType).AsNoTracking();

        var result = new ResponeMode<ContentDto>
        {
            TotalItems = await query.CountAsync(),
            Items = await query
                .Skip(firs)
                .Take(row)
                .Select(x => new ContentDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    CaseType = x.CaseType,
                    VideoUrl = x.VideoUrl,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    ContentType = x.ContentType,
                    ImageUrl = x.ImageUrl,
                }).ToListAsync()
        };

        return result;
    }


    public async Task<bool> ActiveFeedbackAsync(int id, bool isActive)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);

        if (feedback == null)
            return false;

        feedback.Active = isActive; 
        _context.Feedbacks.Update(feedback);
        if (await _context.SaveChangesAsync() > 0)
            return true;
        return false;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return false;

        if(username == "admin" && password == "admin@#77718")
            return true;

        return false;
    }
}
