using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicomTicManagement.Database;
using static UnicomTicManagement.Model.Timtable;

namespace UnicomTicManagement.Controller
{
    internal class TimtableController
    {
        public string AddTimetable(Timetable t)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                    string sql = @"
                INSERT INTO Timetables (TeacherId, HallName, ClassDate, StartTime, EndTime)
                VALUES (@TeacherId, @HallName, @ClassDate, @StartTime, @EndTime)";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TeacherId", t.TeacherId);
                        cmd.Parameters.AddWithValue("@HallName", t.HallName);
                        cmd.Parameters.AddWithValue("@ClassDate", t.ClassDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@StartTime", t.StartTime.ToString(@"hh\:mm"));
                        cmd.Parameters.AddWithValue("@EndTime", t.EndTime.ToString(@"hh\:mm"));
                        cmd.ExecuteNonQuery();
                    }

                    return "Timetable added.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public List<Timetable> GetAllTimetables()
        {
            var list = new List<Timetable>();

            using (var conn = DbCon.GetConnection())
            {
                string query = @"
            SELECT t.TimetableId, t.TeacherId, tr.Name AS TeacherName, t.HallName, 
                   t.ClassDate, t.StartTime, t.EndTime
            FROM Timetables t
            INNER JOIN Users tr ON tr.UserId = (
                SELECT u.UserId FROM Teachers te INNER JOIN Users u ON te.UserId = u.UserId
                WHERE te.TeacherId = t.TeacherId
            )
            ORDER BY t.ClassDate, t.StartTime";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Timetable
                        {
                            TimetableId = Convert.ToInt32(reader["TimetableId"]),
                            TeacherId = Convert.ToInt32(reader["TeacherId"]),
                            TeacherName = reader["TeacherName"].ToString(),
                            HallName = reader["HallName"].ToString(),
                            ClassDate = DateTime.Parse(reader["ClassDate"].ToString()),
                            StartTime = TimeSpan.Parse(reader["StartTime"].ToString()),
                            EndTime = TimeSpan.Parse(reader["EndTime"].ToString())
                        });
                    }
                }
            }

            return list;
        }

        public string DeleteTimetable(int timetableId)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                    string sql = "DELETE FROM Timetables WHERE TimetableId = @Id";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", timetableId);
                        cmd.ExecuteNonQuery();
                    }
                    return "Timetable deleted.";
                }
            }
            catch (Exception ex)
            {
                return "Error deleting: " + ex.Message;
            }
        }
    }
}
