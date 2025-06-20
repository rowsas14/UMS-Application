using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Model
{
    internal class Admin:User
    {
        public int AdminId { get; set; }
        public string Designation { get; set; }  
    }
}
