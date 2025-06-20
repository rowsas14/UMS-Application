using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Model
{
    internal class Teacher:User
    {
        public int TeacherId { get; set; } 
        public string Department { get; set; }
        public string Designation { get; set; }
    }
}

