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
    internal class AdminController
    {
        public List<Admin> GetAllAdmins()
        {
            var adminList = new List<Admin>();

            using (var conn = DbCon.GetConnection())
            {
                string query = @"
                SELECT a.AdminId, a.Designation,
                       u.UserId, u.Name, u.Username, u.Password, u.Email, u.Role
                FROM Admin a
                INNER JOIN Users u ON a.UserId = u.UserId";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        adminList.Add(new Admin
                        {
                            AdminId = Convert.ToInt32(reader["AdminId"]),
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

            return adminList;
        }


        public string UpdateAdmin(Admin admin)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                    
                    string updateUserSql = @"
                    UPDATE Users
                    SET Name = @Name, Username = @Username, Password = @Password, Email = @Email
                    WHERE UserId = @UserId";

                    using (var cmd = new SQLiteCommand(updateUserSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", admin.Name);
                        cmd.Parameters.AddWithValue("@Username", admin.Username);
                        cmd.Parameters.AddWithValue("@Password", admin.Password);
                        cmd.Parameters.AddWithValue("@Email", admin.Email);
                        cmd.Parameters.AddWithValue("@UserId", admin.UserId);
                        cmd.ExecuteNonQuery();
                    }

                    string updateAdminSql = @"
                    UPDATE Admin
                    SET Designation = @Designation
                    WHERE AdminId = @AdminId";

                    using (var cmd = new SQLiteCommand(updateAdminSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Designation", admin.Designation);
                        cmd.Parameters.AddWithValue("@AdminId", admin.AdminId);
                        cmd.ExecuteNonQuery();
                    }

                    return "Admin updated successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error updating admin: " + ex.Message;
            }
        }

 
        public string DeleteAdmin(int adminId, int userId)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                
                    string deleteAdminSql = "DELETE FROM Admin WHERE AdminId = @AdminId";
                    using (var cmd = new SQLiteCommand(deleteAdminSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@AdminId", adminId);
                        cmd.ExecuteNonQuery();
                    }

                   
                    string deleteUserSql = "DELETE FROM Users WHERE UserId = @UserId";
                    using (var cmd = new SQLiteCommand(deleteUserSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.ExecuteNonQuery();
                    }

                    return "Admin deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error deleting admin: " + ex.Message;
            }
        }

    }
}
