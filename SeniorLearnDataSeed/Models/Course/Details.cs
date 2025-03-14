﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeniorLearnDataSeed.Data.Core;
using SeniorLearnDataSeed.Models.Session;

namespace SeniorLearnDataSeed.Models.Course
{
    public class Details
    {
        public Details() { }

        public Details(Data.Core.Course c)
        {
            CourseId = c.CourseId;
            Name = c.Name;
            Description = c.Description;
            Category = c.Category;
            CreatorName = c.CreatorName;
            SelectedDifficulty = c.Difficulty;
            ApplicationUserId = c.ApplicationUserId;

            Sessions = c.Sessions.Select(s => new Session.SessionDetails(s)).ToList();
          
            isStandAlone = c.isStandAlone;

        }
        [Key]
        public int CourseId { get; set; }

        public string Name { get; set; } = default!;
        // public List<Session> Sessions { get; set; } 

        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;

       public string CreatorName { get; set; } = default!;  
        
        public string SelectedDifficulty { get; set; }


        public string ApplicationUserId { get; set; }

        //   [Display(Description = "Created By (Member Name):")]
         public string MemberName { get; set; } 

        //public int SessionsCount { get; set; }

        public List<Models.Session.SessionDetails> Sessions { get; set; }
        public bool isStandAlone { get; set; }

        // Additional properties or calculations < to discuss in next code review if we would like to keep these.

        //  public int NameLength => Name.Length;




    }
}


