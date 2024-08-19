﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeniorLearnConsole;

#nullable disable

namespace SeniorLearnConsole.Migrations
{
    [DbContext(typeof(MyAppContext))]
    [Migration("20240818033148_sessointocourse")]
    partial class sessointocourse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SeniorLearnConsole.Tables.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SeniorLearnConsole.Tables.Member", b =>
                {
                    b.Property<int>("memberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("memberId"));

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("memberId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("SeniorLearnConsole.Tables.MemberSession", b =>
                {
                    b.Property<int>("memberId")
                        .HasColumnType("int");

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.HasKey("memberId", "courseId");

                    b.ToTable("MemberSession");
                });

            modelBuilder.Entity("SeniorLearnConsole.Tables.Session", b =>
                {
                    b.Property<int>("sessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("sessionId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("sessionId");

                    b.HasIndex("CourseId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("SeniorLearnConsole.Tables.MemberSession", b =>
                {
                    b.HasOne("SeniorLearnConsole.Tables.Course", "course")
                        .WithMany("MemberSession")
                        .HasForeignKey("memberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeniorLearnConsole.Tables.Member", "member")
                        .WithMany("MemberSession")
                        .HasForeignKey("memberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("member");
                });

            modelBuilder.Entity("SeniorLearnConsole.Tables.Session", b =>
                {
                    b.HasOne("SeniorLearnConsole.Tables.Course", "Course")
                        .WithMany("Sessions")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("SeniorLearnConsole.Tables.Course", b =>
                {
                    b.Navigation("MemberSession");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("SeniorLearnConsole.Tables.Member", b =>
                {
                    b.Navigation("MemberSession");
                });
#pragma warning restore 612, 618
        }
    }
}
