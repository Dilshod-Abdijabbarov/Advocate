using Advocate.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Advocate.Context
{
    public class AdvocateDbContext : DbContext
    {
        public AdvocateDbContext(DbContextOptions<AdvocateDbContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<WorkHistory> WorkHistories { get; set; }
    }
}
