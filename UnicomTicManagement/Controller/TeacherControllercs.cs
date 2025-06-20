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
    internal class TeacherControllercs
    {
        public List<Teacher> GetAllTeachers()
        {
            var teachers = new List<Teacher>();

            using (var conn = DbCon.GetConnection())
            {
                string query = @"
                SELECT t.TeacherId, t.Department, t.Designation,
                       u.UserId, u.Name, u.Username, u.Password, u.Email, u.Role
                FROM Teachers t
                INNER JOIN Users u ON t.UserId = u.UserId";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teachers.Add(new Teacher
                        {
                            TeacherId = Convert.ToInt32(reader["TeacherId"]),
                            Department = reader["Department"].ToString(),
                            Designation = reader["Designation"].ToString(),
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

            return teachers;
        }



        public string UpdateTeacher(Teacher teacher)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                   
                    string updateUserQuery = @"
                    UPDATE Users
                    SET Name = @Name, Username = @Username, Password = @Password, Email = @Email
                    WHERE UserId = @UserId";

                    using (var cmd = new SQLiteCommand(updateUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", teacher.Name);
                        cmd.Parameters.AddWithValue("@Username", teacher.Username);
                        cmd.Parameters.AddWithValue("@Password", teacher.Password);
                        cmd.Parameters.AddWithValue("@Email", teacher.Email);
                        cmd.Parameters.AddWithValue("@UserId", teacher.UserId);
                        cmd.ExecuteNonQuery();
                    }

                    
                    string updateTeacherQuery = @"
                    UPDATE Teachers
                    SET Department = @Department, Designation = @Designation
                    WHERE TeacherId = @TeacherId";

                    using (var cmd = new SQLiteCommand(updateTeacherQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Department", teacher.Department);
                        cmd.Parameters.AddWithValue("@Designation", teacher.Designation);
                        cmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherId);
                        cmd.ExecuteNonQuery();
                    }

                    return "Teacher updated successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error updating teacher: " + ex.Message;
            }
        }


        public string DeleteTeacher(int teacherId, int userId)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                   
                    string deleteTeacherQuery = "DELETE FROM Teachers WHERE TeacherId = @TeacherId";
                    using (var cmd = new SQLiteCommand(deleteTeacherQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                        cmd.ExecuteNonQuery();
                    }

                    
                    string deleteUserQuery = "DELETE FROM Users WHERE UserId = @UserId";
                    using (var cmd = new SQLiteCommand(deleteUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.ExecuteNonQuery();
                    }

                    return "Teacher deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error deleting teacher: " + ex.Message;
            }
        }


    }
}
