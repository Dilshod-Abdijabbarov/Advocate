using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Advocate.Models.enums;

namespace Advocate.Models.Entity;

[Table("feedbacks")]
public class Feedback
{
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(30)]
    [Column("first_name")]
    public string FirstName { get; set; }

    [MaxLength(30)]
    [Column("last_name")]
    public string LastName { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("discription")]
    public string Discription { get; set; }

    [Column("case_type")]
    public CaseType CaseType { get; set; }

    [Column("active")]
    public bool Active { get; set; } = false;

    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(5);
}
