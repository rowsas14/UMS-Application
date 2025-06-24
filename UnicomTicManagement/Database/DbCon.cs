using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicomTicManagement.Database
{
    public static class DbCon
    {
        private static string  connectionString = "Data Source=UnicomTic.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            conn.Open();
            //MessageBox.Show("Done ok");
            return conn;
        }

    }
}
