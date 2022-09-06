using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class storyDAO
    {
        public DataTable getHistory()
        {
            DataTable table = new DataTable();
            table.Columns.Add("EnemyKilled", typeof(int));
            table.Columns.Add("HeroLevel", typeof(int));
            table.Columns.Add("Hard", typeof(string));
            table.Columns.Add("Time", typeof(DateTime));
           

            string sql = "select * from HistoryG";
            var reader = new DBCONNECT().getData(sql);
            while (reader.Read())
            {
                var EnemyKilled = int.Parse( reader[0].ToString());
                var HeroLevel = int.Parse(reader[1].ToString());
                var Hard =reader[2].ToString();
                var Time = DateTime.Parse(reader[3].ToString());
                table.Rows.Add(EnemyKilled,HeroLevel,Hard,Time);
            }
            return table;
        }

        public int insertPre(History h)
        {
            int n = 0;
            // không cần chuyển đổi giá trị do tự convert
            // int k = pro.Status == true ? 1 : 0;
            // set các tham số dạng @name tương ứng với số các thuộc tính
            //ODA dùng tên tham số nên nếu thích thì đặt @1, @2 cho nhanh
            string sql = "insert into HistoryG " +
                " values(@EnemyKilled,@Herolevel,@hard,@timeAccess)";
            using (var SqlConnec = new DBCONNECT().getConnect())
            {
                SqlConnec.Open();
                using (var command = SqlConnec.CreateCommand())
                {
                    command.CommandText = sql;
                    
                    command.Parameters.Add(new SqlParameter("@EnemyKilled", h.Enk));
                    command.Parameters.Add(new SqlParameter("@Herolevel", h.Level));
                  
                    command.Parameters.Add(new SqlParameter("@hard", h.Hard));
                    command.Parameters.Add(new SqlParameter("@timeAccess", h.TimeAccess));
                    n = command.ExecuteNonQuery();
                }

            }
            return n;
        }




    }
}
