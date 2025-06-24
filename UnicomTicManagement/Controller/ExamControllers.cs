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
    internal class ExamControllers
    {
        public string AddExam(Exam exam)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                    string insertQuery = @"
                INSERT INTO Exams (StudentId, SubjectId, ExamName, ExamType, Marks, ExamDate)
                VALUES (@StudentId, @SubjectId, @ExamName, @ExamType, @Marks, @ExamDate)";

                    using (var cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", exam.StudentId);
                        cmd.Parameters.AddWithValue("@SubjectId", exam.SubjectId);
                        cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                        cmd.Parameters.AddWithValue("@ExamType", exam.ExamType);
                        cmd.Parameters.AddWithValue("@Marks", exam.Marks);
                        cmd.Parameters.AddWithValue("@ExamDate", exam.ExamDate.ToString("yyyy-MM-dd"));

                        cmd.ExecuteNonQuery();
                    }

                    return "Exam added successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error adding exam: " + ex.Message;
            }
        }

       

        public List<Exam> GetExamRecords()
        {
            var list = new List<Exam>();

            using (var conn = DbCon.GetConnection())
            {
                string query = @"
                SELECT e.ExamId, 
                       u.Name AS StudentName, 
                       s.SubjectName,
                       e.ExamName, 
                       e.ExamType, 
                       e.Marks, 
                       e.ExamDate
                FROM Exams e
                INNER JOIN Students st ON e.StudentId = st.StudentId
                INNER JOIN Users u ON st.UserId = u.UserId
                INNER JOIN Subjects s ON e.SubjectId = s.SubjectId
                ORDER BY u.Name, s.SubjectName, e.ExamDate";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Exam
                        {
                            ExamId = Convert.ToInt32(reader["ExamId"]),
                            StudentName = reader["StudentName"].ToString(),
                            SubjectName = reader["SubjectName"].ToString(),
                            ExamName = reader["ExamName"].ToString(),
                            ExamType = reader["ExamType"].ToString(),
                            Marks = Convert.ToInt32(reader["Marks"]),
                            ExamDate = DateTime.Parse(reader["ExamDate"].ToString())
                        });
                    }
                }
            }

            return list;

        }


        public string UpdateExam(Exam exam)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                    string updateQuery = @"
                UPDATE Exams
                SET StudentId = @StudentId, SubjectId = @SubjectId,
                    ExamName = @ExamName,
                    ExamType = @ExamType,
                    Marks = @Marks,
                    ExamDate = @ExamDate
                WHERE ExamId = @ExamId";

                    using (var cmd = new SQLiteCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", exam.StudentId);
                        cmd.Parameters.AddWithValue("@SubjectId", exam.SubjectId);
                        cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                        cmd.Parameters.AddWithValue("@ExamType", exam.ExamType);
                        cmd.Parameters.AddWithValue("@Marks", exam.Marks);
                        cmd.Parameters.AddWithValue("@ExamDate", exam.ExamDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@ExamId", exam.ExamId);

                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0 ? "Exam updated successfully." : "Update failed.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error updating exam: " + ex.Message;
            }
        }




        public string DeleteExam(int examId)
        {
            try
            {
                using (var conn = DbCon.GetConnection())
                {
                    string deleteQuery = "DELETE FROM Exams WHERE ExamId = @ExamId";

                    using (var cmd = new SQLiteCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ExamId", examId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0 ? "Exam deleted successfully." : "Delete failed.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error deleting exam: " + ex.Message;
            }
        }




    }



}
 
