using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Model
{
    internal class Exam
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public string ExamName { get; set; }     
        public string ExamType { get; set; }     

        public int Marks { get; set; }
        public DateTime ExamDate { get; set; }
    }
}




