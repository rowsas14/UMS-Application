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
    internal class CourseController
    {
        public List<Course> GetAllCourses()
        {
            var courseList = new List<Course>();

            using (var conn = DbCon.GetConnection())
            {
                string query = "SELECT CourseId, CourseName FROM Courses";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courseList.Add(new Course
                        {
                            CourseId = Convert.ToInt32(reader["CourseId"]),
                            CourseName = reader["CourseName"].ToString()
                        });
                    }
                }
            }

            return courseList;
        }

        public string AddCourse(Course course)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                    string query = "INSERT INTO Courses (CourseName) VALUES (@name)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", course.CourseName);
                        cmd.ExecuteNonQuery();
                    }

                    return "Course added successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


       
    }
}
