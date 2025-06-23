using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicomTicManagement.Database;
using UnicomTicManagement.Model;

namespace UnicomTicManagement.Controller
{
    internal class SubjectController
    {
        public List<Subject> GetAllSubjects()
        {
            var subjects = new List<Subject>();

            using (var conn = DbCon.GetConnection())
            {
                string query = @"
                SELECT s.SubjectId, s.SubjectName, s.CourseId, c.CourseName
                FROM Subjects s
                INNER JOIN Courses c ON s.CourseId = c.CourseId";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subjects.Add(new Subject
                        {
                            SubjectId = Convert.ToInt32(reader["SubjectId"]),
                            SubjectName = reader["SubjectName"].ToString(),
                            CourseId = Convert.ToInt32(reader["CourseId"]),
                            CourseName = reader["CourseName"].ToString()
                        });
                    }
                }
            }

            return subjects;
        }

        public string AddSubject(Subject subject)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                    string query = "INSERT INTO Subjects (SubjectName, CourseId) VALUES (@name, @courseId)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", subject.SubjectName);
                        cmd.Parameters.AddWithValue("@courseId", subject.CourseId);
                        cmd.ExecuteNonQuery();
                    }

                    return "Subject added successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
