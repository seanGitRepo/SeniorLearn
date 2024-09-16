using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Data.Core;

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
            //modelBuilder.Entity<Session>()
            //   .HasDiscriminator<string>("session_type")
            //   .HasValue<Session>("session_base")
            //   .HasValue<OnPremSession>("session_onprem")
            //   .HasValue<OnlineSession>("session_online");
            //modelBuilder.Entity<Member>().HasData(
            //   new Member
            //   {
            //       MemberId = 1,
            //       FirstName = "Saxon",
            //       LastName = "Cadet",
            //       Email = "saxon.cadet@gelos.com",
            //       Type = MemberType.Pro,
            //       StartDate = DateTime.Now,
            //   },
            //   new Member
            //   {
            //       MemberId = 2,
            //       FirstName = "Sean",
            //       LastName = "Saap",
            //       Email = "sean.saap@gelos.com",
            //       Type = MemberType.Standard,
            //       StartDate = DateTime.Now,
            //   });
            //modelBuilder.Entity<Course>().HasData(
            //    new Course
            //    {
            //        CourseId = 1,
            //        Name = "Gardening",
            //        Description = "Beginner's guide to gardening",
            //        MemberId = 1,
            //        isStandAlone = true
            //    });
            //modelBuilder.Entity<Enrollment>().HasData(
            //    new Enrollment
            //    {
            //        EnrollmentId = 1,
            //        MemberId = 2,
            //        SessionId = 1,

            //    },
            //    new Enrollment
            //    {
            //        EnrollmentId = 2,
            //        MemberId = 2,
            //        SessionId = 2
            //    }
            //   );
            //modelBuilder.Entity<OnlineSession>().HasData(
            //    new OnlineSession
            //    {
            //        SessionId = 1,
            //        Date = DateTime.Now,
            //        CourseId = 1,
            //        Status = SessionStatus.Scheduled,
            //        OnlineLink = "microsoft.com",
            //        AdditionalDetails = "Cameras are required"

            //    });
            //modelBuilder.Entity<OnPremSession>().HasData(
            //    new OnPremSession
            //    {
            //        SessionId = 2,
            //        Date = DateTime.Now,
            //        CourseId = 2,
            //        Status = SessionStatus.Scheduled,
            //        StreetNumber = "25A",
            //        StreetName = "Pitt Street",
            //        Suburb = "Sydney"

            //    });
            //modelBuilder.Entity<Payment>().HasData(
            //    new Payment
            //    {
            //        PaymentId = 1,
            //        PaymentType = PaymentClassifications.Cash,
            //        AmountPaid = 100.00,
            //        MemberId = 1
            //    },
            //    new Payment
            //    {
            //        PaymentId = 2,
            //        PaymentType = PaymentClassifications.EFT,
            //        AmountPaid = 150.00,
            //        MemberId = 2
            //    });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
