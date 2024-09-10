using Microsoft.EntityFrameworkCore;

namespace SeniorLearnDataSeed.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Member>()
            //    .HasOne(m=>m.Payment)
            //    .WithOne(p.)

            //modelBuilder.Entity<Course>()
            //    .HasOne(c => c.Organiser)
            //    .WithMany(m => m.CreatedCourses)
            //    .HasForeignKey(c => c.MemberId)
            //    .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
