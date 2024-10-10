using System.ComponentModel.DataAnnotations;

namespace SeniorLearnDataSeed.Models.Session
{
    public class SessionEdit:SessionCreate
    {
        [Key]
        public int SessionId { get; set; }
      

      
    }
}
