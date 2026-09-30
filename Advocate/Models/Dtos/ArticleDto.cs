using Advocate.Models.enums;

namespace Advocate.Models.Dtos;

public class ArticleDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Discription { get; set; }
    public string ImageUrl { get; set; }
    public CaseType CaseType { get; set; }
    public DateTime CreatedDate { get; set; }

    public bool Active { get; set; } = true;
}
