using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Model
{
    public class Student:User
    {
        public int StudentId { get; set; } 

        public string StudentNumber { get; set; }
        public string Department { get; set; }
    }
}
