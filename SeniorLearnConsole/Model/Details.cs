using SeniorLearnConsole.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeniorLearnConsole.Model
{
    public class Details {

        public Details() { }

    public Details(Data.Member m)
    {

        memberId = m.memberId;
        firstName = m.FirstName;
        lastName = m.LastName ;
        }

    public int memberId { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    }
}
