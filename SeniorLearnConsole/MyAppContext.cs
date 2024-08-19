using Microsoft.EntityFrameworkCore;
using SeniorLearnConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SeniorLearnConsole
{
    public class MyAppContext : DbContext
    {
        public DbSet<Member> Members { get; set; }

       // public DbSet<NormalMember> NormalMembers { get; set; }
       // public DbSet<ProMember> ProMembers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Session>Sessions { get; set; }
        public DbSet<MemberCourse> MemberSession { get; set; }
       // public DbSet<ProMember> ProMembers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MemberCourse>()
                .HasKey(ms => new { ms.memberId, ms.CourseId });
            modelBuilder.Entity<MemberCourse>()
                .HasOne(c => c.course)
                .WithMany(ms => ms.MemberCourse)
                .HasForeignKey(c => c.CourseId);
            modelBuilder.Entity<MemberCourse>()
               .HasOne(m => m.member)
               .WithMany(ms => ms.MemberCourse)
               .HasForeignKey(c => c.memberId);

            //modelBuilder.Entity<Member>()
            //    .HasDiscriminator<string>("Member_Type")
            //    .HasValue<Member>("member_base")
            //    .HasValue<ProMember>("member_pro");


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string cs = "Data Source=DESKTOP-3RBE7T2;Initial Catalog=ConsoleDBSeniorLearn;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;";
            optionsBuilder.UseSqlServer(cs);
        }
    }
}
