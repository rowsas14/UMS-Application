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
    internal class StaffController
    {
        public List<Staff> GetAllStaff()
        {
            var staffList = new List<Staff>();

            using (var conn = DbCon.GetConnection())
            {
                string query = @"
                SELECT s.StaffId, s.Designation,
                       u.UserId, u.Name, u.Username, u.Password, u.Email, u.Role
                FROM Staffs s
                INNER JOIN Users u ON s.UserId = u.UserId";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        staffList.Add(new Staff
                        {
                            StaffId = Convert.ToInt32(reader["StaffId"]),
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

            return staffList;
        }


        public string AddStaff(Staff staff)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                using (var transaction = conn.BeginTransaction())
                {
                    string userQuery = @"
                INSERT INTO Users (Name, Username, Password, Email, Role)
                VALUES (@Name, @Username, @Password, @Email, @Role);
                SELECT last_insert_rowid();";

                    int userId;
                    using (var cmd = new SQLiteCommand(userQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", staff.Name);
                        cmd.Parameters.AddWithValue("@Username", staff.Username);
                        cmd.Parameters.AddWithValue("@Password", staff.Password);
                        cmd.Parameters.AddWithValue("@Email", staff.Email);
                        cmd.Parameters.AddWithValue("@Role", "Staff");

                        userId = Convert.ToInt32(cmd.ExecuteScalar()); 
                    }

                    string staffQurey = @"
                INSERT INTO Staffs (UserId, Designation)
                VALUES (@UserId, @Designation);";

                    using (var cmd = new SQLiteCommand(staffQurey, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Designation", staff.Designation);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return "Staff added successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error adding staff: " + ex.Message;
            }
        }


        public string UpdateStaff(Staff staff)
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
                        cmd.Parameters.AddWithValue("@Name", staff.Name);
                        cmd.Parameters.AddWithValue("@Username", staff.Username);
                        cmd.Parameters.AddWithValue("@Password", staff.Password);
                        cmd.Parameters.AddWithValue("@Email", staff.Email);
                        cmd.Parameters.AddWithValue("@UserId", staff.UserId);
                        cmd.ExecuteNonQuery();
                    }

                   
                    string updateStaffQuery = @"
                    UPDATE Staffs
                    SET Designation = @Designation
                    WHERE StaffId = @StaffId";

                    using (var cmd = new SQLiteCommand(updateStaffQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Designation", staff.Designation);
                        cmd.Parameters.AddWithValue("@StaffId", staff.StaffId);
                        cmd.ExecuteNonQuery();
                    }

                    return "Staff updated successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error updating staff: " + ex.Message;
            }
        }

        public string DeleteStaff(int staffId, int userId)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                    
                    string deleteStaffQuery = "DELETE FROM Staffs WHERE StaffId = @StaffId";
                    using (var cmd = new SQLiteCommand(deleteStaffQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@StaffId", staffId);
                        cmd.ExecuteNonQuery();
                    }

                    
                    string deleteUserQuery = "DELETE FROM Users WHERE UserId = @UserId";
                    using (var cmd = new SQLiteCommand(deleteUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.ExecuteNonQuery();
                    }

                    return "Staff deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error deleting staff: " + ex.Message;
            }
        }

    }
}
