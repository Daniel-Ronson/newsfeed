using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;


namespace News.Models
{
    public class LoginContext : DbContext
    {
        public string ConnectionString { get; set; }

        public LoginContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection getConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public string CheckCredentials(string email, string password)
        {
            using(MySqlConnection conn = getConnection())
            {
      
                string sql = $"SELECT  email, password, username FROM user WHERE email = '{email}' AND password = '{password}'";
             //   string sql = $"SELECT  email FROM user WHERE email = {email}";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                string test = reader["username"].ToString();
                conn.Close();
                
                return test;

            }
        }

        public string AddUser(string email, string password, string username, string passwordCheck)
        {
            using (MySqlConnection conn = getConnection())
            {

                string sql = $"INSERT INTO user(username, password, email) VALUES('{username}', '{password}', '{email}')";
                //   string sql = $"SELECT  email FROM user WHERE email = {email}";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    string error = "Username already being used";
                    return error;
                }
                string welcomeStatement = "Welcome" + "{username}";
                conn.Close();

                return welcomeStatement;
            }

            

            }


    }
}
