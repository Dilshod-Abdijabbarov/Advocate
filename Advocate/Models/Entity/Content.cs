using Advocate.Models.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advocate.Models.Entity;

[Table("content")]
public class Content
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("title")]
    public string? Title { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("case_type")]
    public CaseType? CaseType { get; set; }
    [Column("content_type")]
    public ContentType ContentType { get; set; }
    [Column("image_url")]
    public string? ImageUrl { get; set; }
    [Column("video_url")]
    public string? VideoUrl { get; set; }
    [Column("created_date")]
    public DateTime CreatedDate { get; set; } 
}
