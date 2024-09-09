using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Models;

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

            modelBuilder.Entity<Enrollment>()
                .HasKey(c => new { c.MemberId, c.SessionId });
            modelBuilder.Entity<Enrollment>()
                .HasOne(c => c.Session)
                .WithMany(e => e.EnrolledMembers)
                .HasForeignKey(c => c.SessionId);
            modelBuilder.Entity<Enrollment>()
                .HasOne(m => m.Member)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(c => c.MemberId);


            modelBuilder.Entity<MemberCourse>()
                .HasKey(c => new { c.OrganiserId, c.CourseId });
            modelBuilder.Entity<MemberCourse>()
                .HasOne(m => m.Course)
                .WithOne(c => c.MemberCourse);
                
                
            modelBuilder.Entity<MemberCourse>()
                .HasOne(m => m.Organiser)
                .WithMany(e => e.CreatedCourses)
                .HasForeignKey(c => c.OrganiserId);



            modelBuilder.Entity<Member>()
                .Property(m => m.Type)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
