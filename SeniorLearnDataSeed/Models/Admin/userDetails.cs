using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearnDataSeed.Models.Admin
{
    public class userDetails
    {
        public string userName {  get; set; }
        public string firstName { get; set; }
        [Key]
        public string userId { get; set; }
        public string lastName { get; set; }

        public string role {  get; set; }
       
       public List<string>? RoleList { get; set; }

    }
}
