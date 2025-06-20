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
    public class Studentontroller
    {
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (var conn = DbCon.GetConnection())
            {
                string query = @"
                SELECT s.StudentId, s.StudentNumber, s.Department,
                       u.UserId, u.Name, u.Username, u.Password, u.Email, u.Role
                FROM Students s
                INNER JOIN Users u ON s.UserId = u.UserId";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            StudentNumber = reader["StudentNumber"].ToString(),
                            Department = reader["Department"].ToString(),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Name = reader["Name"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Email = reader["Email"].ToString(),
                            Role = reader["Role"].ToString()
                        });
                    }
                }
            }

            return students;
        }


        public string DeleteStudent(int studentId, int userId)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                   
                    string deleteQuery = "DELETE FROM Students WHERE StudentId = @StudentId";
                    using (var cmd1 = new SQLiteCommand(deleteQuery, conn))
                    {
                        cmd1.Parameters.AddWithValue("@StudentId", studentId);
                        cmd1.ExecuteNonQuery();
                    }

                   
                    string deleteUserQuery = "DELETE FROM Users WHERE UserId = @UserId";
                    using (var cmd2 = new SQLiteCommand(deleteUserQuery, conn))
                    {
                        cmd2.Parameters.AddWithValue("@UserId", userId);
                        cmd2.ExecuteNonQuery();
                    }
                }

                return "Student deleted successfully.";
            }
            catch (Exception ex)
            {
                return "Error deleting student: " + ex.Message;
            }

        }


        public string UpdateStudent(Student student)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {

                    string updateUserQuery = @"UPDATE Users SET Name=@Name,Username =@UsereName,Password =@Password,WHERE UserId=@UserId";
                    
                    using (var cmd = new SQLiteCommand(updateUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", student.Name);
                        cmd.Parameters.AddWithValue("@Username", student.Username);
                        cmd.Parameters.AddWithValue("@Password", student.Password);
                        cmd.Parameters.AddWithValue("@Email", student.Email);
                        cmd.Parameters.AddWithValue("@UserId", student.UserId);

                        cmd.ExecuteNonQuery();
                    }

                    string updateStudentQuery = @"
                    UPDATE Students
                    SET StudentNumber = @StudentNumber, Department = @Department
                    WHERE StudentId = @StudentId";

                    using (var cmd = new SQLiteCommand(updateStudentQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
                        cmd.Parameters.AddWithValue("@Department", student.Department);
                        cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                        cmd.ExecuteNonQuery();
                    }

                    return "Student updated successfully.";
                }


                
            }
            catch (Exception ex)
            {
                return "Error updating student: " + ex.Message;
            }
        }

    }
}
