﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Model
{
    internal class Teacher:User
    {     
        //public int UserId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Designation { get; set; }
    }
}

