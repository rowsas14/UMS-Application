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
    public class StudentController
    {
        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();

            using (var conn = DbCon.GetConnection())
            {
                string query = @"
                SELECT s.StudentId, s.StudentNumber, s.CourseId,
                       c.CourseName,
                       u.UserId, u.Name, u.Username, u.Password, u.Email, u.Role
                FROM Students s
                INNER JOIN Users u ON s.UserId = u.UserId
                LEFT JOIN Courses c ON s.CourseId = c.CourseId";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            StudentNumber = reader["StudentNumber"].ToString(),
                            CourseId = reader["CourseId"] != DBNull.Value ? Convert.ToInt32(reader["CourseId"]) : 0,
                            CourseName = reader["CourseName"] != DBNull.Value ? reader["CourseName"].ToString() : "",
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

        public string AddStudent(Student student)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                using (var transaction = conn.BeginTransaction())
                {
                    // Insert into Users
                    string insertUserQuery = @"
                    INSERT INTO Users (Name, Username, Password, Email, Role)
                    VALUES (@Name, @Username, @Password, @Email, @Role);
                    SELECT last_insert_rowid();";

                    long userId;
                    using (var cmd = new SQLiteCommand(insertUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", student.Name);
                        cmd.Parameters.AddWithValue("@Username", student.Username);
                        cmd.Parameters.AddWithValue("@Password", student.Password);
                        cmd.Parameters.AddWithValue("@Email", student.Email);
                        cmd.Parameters.AddWithValue("@Role", student.Role);

                        userId = (long)cmd.ExecuteScalar();
                    }

                    // Insert into Students
                    string insertStudentQuery = @"
                    INSERT INTO Students (UserId, StudentNumber, CourseId)
                    VALUES (@UserId, @StudentNumber, @CourseId);";

                    using (var cmd = new SQLiteCommand(insertStudentQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
                        cmd.Parameters.AddWithValue("@CourseId", student.CourseId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }

                return "Student added successfully.";
            }
            catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
            {
                // Handle UNIQUE constraint violation (username or student number duplicate)
                if (ex.Message.Contains("Users.Username"))
                    return "Error: Username already exists.";
                else if (ex.Message.Contains("Students.StudentNumber"))
                    return "Error: Student number already exists.";
                else
                    return "Database constraint error: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error adding student: " + ex.Message;
            }

        }



        public string DeleteStudent(int studentId, int userId)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                using (var transaction = conn.BeginTransaction())
                {
                    string deleteStudentQuery = "DELETE FROM Students WHERE StudentId = @StudentId";
                    using (var cmd1 = new SQLiteCommand(deleteStudentQuery, conn))
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

                    transaction.Commit();
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
                using (var transaction = conn.BeginTransaction())
                {
                    string updateUserQuery = @"
                    UPDATE Users
                    SET Name = @Name, Username = @Username, Password = @Password, Email = @Email
                    WHERE UserId = @UserId";

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
                    SET StudentNumber = @StudentNumber, CourseId = @CourseId
                    WHERE StudentId = @StudentId";

                    using (var cmd = new SQLiteCommand(updateStudentQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
                        cmd.Parameters.AddWithValue("@CourseId", student.CourseId);
                        cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return "Student updated successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error updating student: " + ex.Message;
            }
        }

        public List<StCoSub> GetStCoSub()
        {
            var list = new List<StCoSub>();

            using (var conn = DbCon.GetConnection())
            {
                string query = @"
                   SELECT u.Name AS StudentName, 
                    c.CourseName, 
                        s.SubjectName
                     FROM Students st
                      INNER JOIN Users u ON st.UserId = u.UserId
                    INNER JOIN Courses c ON st.CourseId = c.CourseId
                    INNER JOIN Subjects s ON s.CourseId = c.CourseId
                   ORDER BY u.Name";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StCoSub
                        {
                            StudentName = reader["StudentName"].ToString(),
                            CourseName = reader["CourseName"].ToString(),
                            SubjectName = reader["SubjectName"].ToString()
                        });
                    }
                }
            }

            return list;
        }



    }

}
