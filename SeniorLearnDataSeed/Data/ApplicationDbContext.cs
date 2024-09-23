using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Course;
using SeniorLearnDataSeed.Models.Session;

namespace SeniorLearnDataSeed.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SeniorLearnDataSeed.Models.Course.Details> Details { get; set; } = default!;
        public DbSet<SeniorLearnDataSeed.Models.Course.Edit> Edit { get; set; } = default!;
        public DbSet<SeniorLearnDataSeed.Models.Session.Details> Details_1 { get; set; } = default!;
    }
}
