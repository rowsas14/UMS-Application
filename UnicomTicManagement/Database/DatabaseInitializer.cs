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

              CREATE TABLE IF NOT EXISTS Courses (
                  CourseId INTEGER PRIMARY KEY AUTOINCREMENT,
                  CourseName TEXT NOT NULL
              );

              CREATE TABLE IF NOT EXISTS Subjects (
                  SubjectId INTEGER PRIMARY KEY AUTOINCREMENT,
                  SubjectName TEXT NOT NULL,
                  CourseId INTEGER NOT NULL,
                  FOREIGN KEY(CourseId) REFERENCES Courses(CourseId)
              );

              CREATE TABLE IF NOT EXISTS Students (
                  StudentId INTEGER PRIMARY KEY AUTOINCREMENT,
                  UserId INTEGER NOT NULL,
                  StudentNumber TEXT UNIQUE NOT NULL,
                  CourseId INTEGER,
                  FOREIGN KEY(UserId) REFERENCES Users(UserId),
                  FOREIGN KEY(CourseId) REFERENCES Courses(CourseId)
              );

              CREATE TABLE IF NOT EXISTS Exams (
                  ExamId INTEGER PRIMARY KEY AUTOINCREMENT,
                  StudentId INTEGER NOT NULL,
                  SubjectId INTEGER NOT NULL,
                  ExamName TEXT NOT NULL,
                  ExamType TEXT NOT NULL,
                  Marks INTEGER,
                  ExamDate TEXT,
                  FOREIGN KEY(StudentId) REFERENCES Students(StudentId),
                  FOREIGN KEY(SubjectId) REFERENCES Subjects(SubjectId)
              );

              CREATE TABLE IF NOT EXISTS Teachers (
                  TeacherId INTEGER PRIMARY KEY AUTOINCREMENT,
                  UserId INTEGER,
                  CourseId INTEGER,
                  Designation TEXT,
                  FOREIGN KEY(UserId) REFERENCES Users(UserId),
                  FOREIGN KEY(CourseId) REFERENCES Courses(CourseId)
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

              CREATE TABLE IF NOT EXISTS Timetables (
                  TimetableId INTEGER PRIMARY KEY AUTOINCREMENT,
                  TeacherId INTEGER NOT NULL,
                  HallName TEXT NOT NULL,
                  ClassDate TEXT NOT NULL,
                  FOREIGN KEY(TeacherId) REFERENCES Teachers(TeacherId)
              );
            ";
                cmd.ExecuteNonQuery();





                MessageBox.Show("Database initialized successfully.", "Info");
            }
        }
    }

}
