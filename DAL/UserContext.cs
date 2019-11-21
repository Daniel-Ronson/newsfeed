using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;


namespace News.Models
{
    public class UserContext
    {
        public string ConnectionString { get; set; }
        public UserContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        private MySqlConnection getConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public User GetUser(int userId = 5)
        {
            userId = 5;
            User user = new User();
            using (MySqlConnection conn = getConnection())
            {
                string sql = $"Select * from user WHERE userid={userId}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {                    
                    user.UserId = Convert.ToInt32(rdr["userid"]);
                    user.Username = rdr["username"].ToString();
                    user.Email = rdr["email"].ToString();
                }                
                conn.Close();
            }
            return user;
        }

        public Boolean checkConnection()
        {
            MySqlConnection con = getConnection();
            try
            {
                con.Open();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
