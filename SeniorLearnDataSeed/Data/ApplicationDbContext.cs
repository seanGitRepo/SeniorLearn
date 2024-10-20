using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Session;
using SeniorLearnDataSeed.Models.Course;
using SeniorLearnDataSeed.Models.Admin;

namespace SeniorLearnDataSeed.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Session>()
                .HasDiscriminator<string>("session_type")
                .HasValue<Session>("session_base")
                .HasValue<OnPremSession>("session_onprem")
                .HasValue<OnlineSession>("session_online");

            modelBuilder.Entity<Session>()
                .Property(s => s.Status)
                .HasConversion<int>();

         

            modelBuilder.Entity<Payment>()
                .Property(x=> x.PaymentType)
                .HasConversion<string>();
            modelBuilder.Entity<Payment>()
               .Property(x => x.PaymentStatus)
               .HasConversion<string>();


            
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
      
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SeniorLearnDataSeed.Models.Admin.userDetails> userDetails { get; set; } = default!;
        
        

       
    }
}
