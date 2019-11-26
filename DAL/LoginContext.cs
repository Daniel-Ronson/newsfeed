using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using News.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

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

        public int CheckCredentials(string email, string password)
        {
            using (MySqlConnection conn = getConnection())
            {
                //CookiesController newCookie = new CookiesController();
                MD5 md5Hash = MD5.Create();
                // bool ReturnData;
                string hashedPassword = GetMd5Hash(md5Hash, password);
                string sql = $"SELECT  userid, email, password, username FROM user WHERE email = '{email}' AND password = '{hashedPassword}'";
                int userId = 0;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                User Client = new User();
                try
                {
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    userId = Convert.ToInt32(rdr["userid"]);
                    // Client.UserId = Convert.ToInt32(rdr["userid"]);
                    //Client.Username = rdr["username"].ToString();
                    //Client.Email = rdr["email"].ToString();

                }
                catch
                {
                    userId = 0;
                    // ReturnData = "User Does not Exist";
                }
                conn.Close();
                return userId;

            }
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string AddUser(string email, string password, string username, string passwordCheck)
        {
            using (MySqlConnection conn = getConnection())
            {

                string sql = $"INSERT INTO user(username, password, email) VALUES('{username}', '{password}', '{email}')";
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
        public string AddSession(int userid, string sessionid)
        {
            using (MySqlConnection conn = getConnection())
            {
                string returnVal;
                string format = "yyyy-MM-dd HH:mm:ss";
                string date = DateTime.Now.ToString(format);
                string sql = $"REPLACE INTO session(userid, sessionid,date) VALUES({userid},'{sessionid}','{date}')";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    returnVal = "Success";
                }
                catch
                {
                    returnVal = "There is an existing session";
                }
                conn.Close();
                return returnVal;
            }


        }
    }
}
