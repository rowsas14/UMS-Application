using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Database;

namespace UnicomTicManagement.Controller
{
    public class LoginController
    {
        private string connectionString = "Data Source=UnicomTic.db;Version=3;";

        public (bool Success, string Role, string Message) Login(string username, string password)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT Role, Password FROM Users WHERE Username = @Username";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPassword = reader["Password"].ToString();

                                // Simple password check, consider hashing for production
                                if (storedPassword == password)
                                {
                                    string role = reader["Role"].ToString();
                                    return (true, role, "Login successful");
                                }
                                else
                                {
                                    return (false, null, "Incorrect password");
                                }
                            }
                            else
                            {
                                return (false, null, "Username not found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, "Error: " + ex.Message);
            }
        }
    }
}
