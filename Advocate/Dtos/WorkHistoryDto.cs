using Advocate.Models.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advocate.Dtos;

public class WorkHistoryDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Discription { get; set; }

    public string DetailedDiscription { get; set; }

    public string ImageUrl { get; set; }

    public CaseType CaseType { get; set; }

    public WorkStatus WorkStatus { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool Active { get; set; }
}
