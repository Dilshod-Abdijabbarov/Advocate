using System.ComponentModel.DataAnnotations.Schema;
using Advocate.Models.enums;

namespace Advocate.Models.Entity;

[Table("work_histories")]
public class WorkHistory
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
   
    [Column("work_status")]
    public WorkStatus WorkStatus { get; set; }

    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("end_date")]
    public DateTime EndDate { get; set; }

    [Column("active")]
    public bool Active { get; set; } = true;

    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(5);
}
