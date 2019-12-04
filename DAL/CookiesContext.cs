using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;


namespace News.DAL
{
    public class CookiesContext
    {
        public string ConnectionString { get; set; }

        public CookiesContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        private MySqlConnection getConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //check if session id is in the database
        //return UserId that is related to the sesion id
        //return 0 if the sesion id is not valid
        public int GetSession(string sessionId)
        {
            int userId;
            using (MySqlConnection conn = getConnection())
            {
                string sql = $"SELECT userid FROM session WHERE sessionid = '{sessionId}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                try
                {
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    userId = Convert.ToInt32(rdr["userid"]);
                }
                catch
                {
                    userId = -1;
                }
                conn.Close();
            }
            return userId;
        }
        public void DeleteSession(string sessionId)
        {
            using (MySqlConnection conn = getConnection())
            {
                string sql = $"DELETE FROM session WHERE sessionid = '{sessionId}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
