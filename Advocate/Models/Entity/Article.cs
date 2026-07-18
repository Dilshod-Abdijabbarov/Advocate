using System.ComponentModel.DataAnnotations.Schema;
using Advocate.Models.enums;

namespace Advocate.Models.Entity;

[Table("articles")]
public class Article
{
    [Column("id")]
    public int Id { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("discription")]
    public string Discription { get; set; }
    [Column("detailed_discription")]
    public string DetailedDiscription { get; set; }
    [Column("image_url")]
    public string ImageUrl { get; set; }
    [Column("case_type")]
    public CaseType CaseType { get; set; }

    [Column("active")]
    public bool Active { get; set; } = true;

    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(5);
}
