using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Model
{
    internal class Timtable
    {

        public class Timetable
        {
            public int TimetableId { get; set; }
            public int TeacherId { get; set; }
            public string TeacherName { get; set; }  
            public string HallName { get; set; }
            public DateTime ClassDate { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
        }
    }
}
