using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Session;

namespace SeniorLearnDataSeed.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            
           
            modelBuilder.Entity<Session>()
               .HasDiscriminator<string>("session_type")
               .HasValue<Session>("session_base")
               .HasValue<OnPremSession>("session_onprem")
               .HasValue<OnlineSession>("session_online");

            modelBuilder.Entity<Session>()
            .Property(s => s.Status)
            .HasConversion<int>();

           

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
      
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SeniorLearnDataSeed.Models.Session.SessionEdit> SessionEdit { get; set; } = default!;
       
    }
}
