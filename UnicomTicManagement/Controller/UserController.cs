using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UnicomTicManagement.Database;
using UnicomTicManagement.Model;

namespace UnicomTicManagement.Controller
{
    public class UserController
    {
        public string RegisterUser(User user)
        {
            try
            {
                int userId;

                using (var conn = DbCon.GetConnection())
                {
                   
                    string userSql = @"
                    INSERT INTO Users (Name, Username, Password, Email, Role)
                    VALUES (@Name, @Username, @Password, @Email, @Role);
                    SELECT last_insert_rowid();";

                    using (var cmd = new SQLiteCommand(userSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Role", user.Role);

                        userId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

               
                    if (user is Student student)
                    {
                        var cmd = new SQLiteCommand("INSERT INTO Students (UserId, StudentNumber, Department) VALUES (@UserId, @StudentNumber, @Department)", conn);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
                        cmd.Parameters.AddWithValue("@Department", student.Department);
                        cmd.ExecuteNonQuery();
                    }
                    else if (user is Teacher teacher)
                    {
                        var cmd = new SQLiteCommand("INSERT INTO Teachers (UserId, Department, Designation) VALUES (@UserId, @Department, @Designation)", conn);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Department", teacher.Department);
                        cmd.Parameters.AddWithValue("@Designation", teacher.Designation);
                        cmd.ExecuteNonQuery();
                    }
                    else if (user is Staff staff)
                    {
                        var cmd = new SQLiteCommand("INSERT INTO Staffs (UserId, Designation) VALUES (@UserId, @Designation)", conn);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Designation", staff.Designation);
                        cmd.ExecuteNonQuery();
                    }
                    else if (user is Admin admin)
                    {
                        var cmd = new SQLiteCommand("INSERT INTO Admin (UserId, Designation) VALUES (@UserId, @Designation)", conn);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Designation", admin.Designation);
                        cmd.ExecuteNonQuery();
                    }

                    return "User registered successfully!";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }


}
  