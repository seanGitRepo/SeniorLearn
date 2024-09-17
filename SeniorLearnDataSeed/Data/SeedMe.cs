using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeniorLearnDataSeed.Data.Core;
using System;

namespace SeniorLearnDataSeed.Data
{
    //public class SeedMe
    //{
    //    private ModelBuilder _mb;
    //    private int _referenceId = 1;
    //    private int _bc = 1;
    //    public SeedMe(ModelBuilder mb)
    //    {
    //        _mb = mb;

    //        foreach (string r in new[] { "LIBRARIAN", "MEMBER" })
    //        {
    //            _mb.Entity<IdentityRole>().HasData(new { Id = $"{r[0]}0112358-1321-3455-8900-000000000042", Name = r, NormalizedName = r });
    //        }
    //        foreach (var l in Genus.Libraries)
    //        {
    //            SeedLibrary(l);
    //        }
    //    }

    //    public class Data
    //    {


    //        public static (int MemberId, string FirstName, string LastName, string Email, MemberType Type, DateTime StartDate)[] Members =>
    //            [
    //            (1, "John", "Smith", "john.smith@email.com", MemberType.Standard, new DateTime(2023, 5, 10)),
    //            (2, "Mary", "Johnson", "mary.j@email.com", MemberType.Standard, new DateTime(2023, 6, 20)),
    //            (3, "James", "Brown", "james.b@email.com", MemberType.Standard, new DateTime(2023, 4, 15)),
    //            (4, "Patricia", "Jones", "patricia.j@email.com", MemberType.Standard, new DateTime(2023, 7, 30)),
    //            (5, "Robert", "Miller", "robert.m@email.com", MemberType.Standard, new DateTime(2023, 3, 12)),
    //            (6, "Jennifer", "Wilson", "jen.w@email.com", MemberType.Standard, new DateTime(2023, 8, 5)),
    //            (7, "Michael", "Moore", "mike.m@email.com", MemberType.Standard, new DateTime(2023, 4, 2)),
    //            (8, "Linda", "Taylor", "linda.t@email.com", MemberType.Standard, new DateTime(2023, 5, 25)),
    //            (9, "William", "Anderson", "will.a@email.com", MemberType.Standard, new DateTime(2023, 6, 11)),
    //            (10, "Elizabeth", "Thomas", "liz.t@email.com", MemberType.Standard, new DateTime(2023, 5, 21)),
    //            (11, "Richard", "Jackson", "richard.j@email.com", MemberType.Standard, new DateTime(2023, 4, 29)),
    //            (12, "Barbara", "White", "barbara.w@email.com", MemberType.Standard, new DateTime(2023, 7, 14)),
    //            (13, "Charles", "Harris", "charles.h@email.com", MemberType.Standard, new DateTime(2023, 8, 8)),
    //            (14, "Susan", "Martin", "susan.m@email.com", MemberType.Standard, new DateTime(2023, 3, 19)),
    //            (15, "Joseph", "Thompson", "joseph.t@email.com", MemberType.Standard, new DateTime(2023, 6, 28)),
    //            (16, "Karen", "Garcia", "karen.g@email.com", MemberType.Standard, new DateTime(2023, 5, 3)),
    //            (17, "Thomas", "Martinez", "tom.m@email.com", MemberType.Standard, new DateTime(2023, 7, 21)),
    //            (18, "Lisa", "Robinson", "lisa.r@email.com", MemberType.Standard, new DateTime(2023, 4, 8)),
    //            (19, "Mark", "Clark", "mark.c@email.com", MemberType.Standard, new DateTime(2023, 6, 24)),
    //            (20, "Nancy", "Lewis", "nancy.l@email.com", MemberType.Standard, new DateTime(2023, 8, 1)),
    //            (21, "Sarah", "Walker", "sarah.w@email.com", MemberType.Pro, new DateTime(2023, 3, 10)),
    //            (22, "Brian", "Hall", "brian.h@email.com", MemberType.Pro, new DateTime(2023, 5, 15)),
    //            (23, "Laura", "Allen", "laura.a@email.com", MemberType.Pro, new DateTime(2023, 4, 23)),
    //            (24, "Steven", "Young", "steven.y@email.com", MemberType.Pro, new DateTime(2023, 7, 17)),
    //            (25, "Amanda", "King", "amanda.k@email.com", MemberType.Pro, new DateTime(2023, 6, 6)),
    //            (26, "Eric", "Wright", "eric.w@email.com", MemberType.Pro, new DateTime(2023, 7, 30)),
    //            (27, "Michelle", "Scott", "michelle.s@email.com", MemberType.Pro, new DateTime(2023, 4, 14)),
    //            (28, "Jason", "Green", "jason.g@email.com", MemberType.Pro, new DateTime(2023, 5, 8)),
    //            (29, "Stephanie", "Adams", "stephanie.a@email.com", MemberType.Pro, new DateTime(2023, 6, 20)),
    //            (30, "Kevin", "Baker", "kevin.b@email.com", MemberType.Pro, new DateTime(2023, 7, 5)),
    //            (31, "Daniel", "Gonzalez", "daniel.g@email.com", MemberType.Honourary, new DateTime(2023, 3, 30)),
    //            (32, "Jessica", "Perez", "jessica.p@email.com", MemberType.Honourary, new DateTime(2023, 4, 10)),
    //            (33, "Paul", "Roberts", "paul.r@email.com", MemberType.Honourary, new DateTime(2023, 5, 12)),
    //            (34, "Emma", "Turner", "emma.t@email.com", MemberType.Honourary, new DateTime(2023, 6, 18)),
    //            (35, "George", "Phillips", "george.p@email.com", MemberType.Honourary, new DateTime(2023, 7, 25))
    //            ];

    //        public static (int CourseId, string Name, string Description, int MemberId, bool isStandAlone)[] Courses =>
    //        [
    //            (1, "Gardening for Beginners", "Learn the basics of gardening", 7, true),
    //            (2, "Introduction to Photography", "Take amazing photos with your camera", 13, true),
    //            (3, "Advanced Knitting Techniques", "Explore new knitting patterns", 9, false),
    //            (4, "Basic Computer Skills", "Learn how to use computers", 21, false),
    //            (5, "Yoga for Seniors", "Stay flexible and fit", 29, false),
    //            (6, "Painting with Watercolors", "Express your creativity through art", 31, false),
    //            (7, "Spanish Language for Beginners", "Start learning conversational Spanish", 18, false)
    //        ];

    //        public static (int SessionId, int CourseId, DateTime Date, SessionStatus Status, string SessionType, string? Location, string? OnlineLink)[] Sessions =>
    //        [
    //            (1, 3, new DateTime(2024, 2, 15), SessionStatus.Scheduled, "OnPrem", "10 Main St, SuburbName", null),
    //            (2, 3, new DateTime(2024, 3, 10), SessionStatus.Scheduled, "OnPrem", "10 Main St, SuburbName", null),
    //            (3, 4, new DateTime(2024, 2, 20), SessionStatus.Scheduled, "Online", null, "www.zoom.com/abcd"),
    //            (4, 4, new DateTime(2024, 3, 5), SessionStatus.Scheduled, "Online", null, "www.zoom.com/efgh"),
    //            (5, 5, new DateTime(2024, 2, 25), SessionStatus.Scheduled, "OnPrem", "25 High St, AnotherSuburb", null),
    //            (6, 5, new DateTime(2024, 3, 12), SessionStatus.Scheduled, "OnPrem", "25 High St, AnotherSuburb", null),
    //            (7, 6, new DateTime(2024, 3, 1), SessionStatus.Scheduled, "OnPrem", "33 Art St, SuburbName", null),
    //            (8, 7, new DateTime(2024, 3, 8), SessionStatus.Scheduled, "Online", null, "www.zoom.com/ijkl")
    //        ];
    //    }
        
    //    }
}
