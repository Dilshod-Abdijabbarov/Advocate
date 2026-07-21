using Advocate.Context;
using Advocate.Dtos;
using Advocate.Interfaces;
using Advocate.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Advocate.Services;

public class FeedbackService : IFeedbackService
{
    public readonly AdvocateDbContext _context;
    public FeedbackService(AdvocateDbContext context)
    {
        _context = context;
    }
    public async Task<bool> CreateFeedbackAsync(FeedbackDto feedbackDto)
    {      
        var feedback = new Feedback
        {
            Active = true,
            FirstName = feedbackDto.FirstName,
            LastName = feedbackDto.LastName,
            CaseType = feedbackDto.CaseType,
            Discription = feedbackDto.Discription,
            Title = feedbackDto.Title,
            CreatedDate = DateTime.UtcNow.AddHours(50)
        };

        await _context.AddAsync(feedback);

        if(await _context.SaveChangesAsync() > 0)
            return true;

        return false;
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
                CaseType = x.CaseType,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Discription = x.Discription,
            }).ToListAsync();

        return feedbacks;
    }
}
