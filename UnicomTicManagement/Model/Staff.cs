using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Model
{
    internal class Staff:User
    {

        public int StaffId { get; set; }
        public string Designation { get; set; }
    }
}
