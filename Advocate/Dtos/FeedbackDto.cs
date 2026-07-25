using Advocate.Models.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advocate.Dtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Discription { get; set; }

        public CaseType CaseType { get; set; }
        public bool Active { get; set; }
    }
}
