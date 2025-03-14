﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeniorLearnDataSeed.Data;

#nullable disable

namespace SeniorLearnDataSeed.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240916082236_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isStandAlone")
                        .HasColumnType("bit");

                    b.HasKey("CourseId");

                    b.HasIndex("MemberId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrollmentId"));

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("MemberId");

                    b.HasIndex("SessionId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("MemberId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<double>("AmountPaid")
                        .HasColumnType("float");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("MemberId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionId"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("SessionId");

                    b.HasIndex("CourseId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Course", b =>
                {
                    b.HasOne("SeniorLearnDataSeed.Data.Core.Member", "Member")
                        .WithMany("CreatedCourses")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Enrollment", b =>
                {
                    b.HasOne("SeniorLearnDataSeed.Data.Core.Member", "Member")
                        .WithMany("Enrollments")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeniorLearnDataSeed.Data.Core.Session", "Session")
                        .WithMany("EnrolledMembers")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Payment", b =>
                {
                    b.HasOne("SeniorLearnDataSeed.Data.Core.Member", "Member")
                        .WithMany("Payments")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Session", b =>
                {
                    b.HasOne("SeniorLearnDataSeed.Data.Core.Course", "Course")
                        .WithMany("Sessions")
                        .HasForeignKey("CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Course", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Member", b =>
                {
                    b.Navigation("CreatedCourses");

                    b.Navigation("Enrollments");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("SeniorLearnDataSeed.Data.Core.Session", b =>
                {
                    b.Navigation("EnrolledMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
