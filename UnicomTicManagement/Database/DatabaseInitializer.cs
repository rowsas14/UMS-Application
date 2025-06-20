using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicomTicManagement.Database
{
    public static class DatabaseInitializer
    {
        public static void CreateTables()
        {
            using (var conn = DbCon.GetConnection())
            {
                var cmd = conn.CreateCommand();

                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Username TEXT UNIQUE NOT NULL,
                    Password TEXT NOT NULL,
                    Email TEXT,
                    Role TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Students (
                    StudentId INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserId INTEGER,
                    StudentNumber TEXT UNIQUE NOT NULL,
                    Department TEXT,
                    FOREIGN KEY(UserId) REFERENCES Users(UserId)
                );

                CREATE TABLE IF NOT EXISTS Teachers (
                    TeacherId INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserId INTEGER,
                    Department TEXT,
                    Designation TEXT,
                    FOREIGN KEY(UserId) REFERENCES Users(UserId)
                );

                CREATE TABLE IF NOT EXISTS Staffs (
                    StaffId INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserId INTEGER,
                    Designation TEXT,
                    FOREIGN KEY(UserId) REFERENCES Users(UserId)
                );

                CREATE TABLE IF NOT EXISTS Admin (
                    AdminId INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserId INTEGER,
                    Designation TEXT,
                    FOREIGN KEY(UserId) REFERENCES Users(UserId)
                );
            ";

                cmd.ExecuteNonQuery();

                MessageBox.Show("Database initialized successfully.", "Info");
            }
        }
    }

}
