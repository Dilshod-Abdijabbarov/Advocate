using Advocate.Models.enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using ContentType = Advocate.Models.enums.ContentType;

namespace Advocate.Models.Dtos
{
    public class ContentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ContentType ContentType { get; set; }
        public CaseType? CaseType { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
