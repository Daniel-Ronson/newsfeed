﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


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

        public bool CheckCredentials(string email, string password)
        {
            string test = null;
            
            using(MySqlConnection conn = getConnection())
            {
                MD5 md5Hash = MD5.Create();
                
                string hashedPassword = GetMd5Hash(md5Hash, password);
                string sql = $"SELECT  email, password, username FROM user WHERE email = '{email}' AND password = '{hashedPassword}'";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    test = reader["username"].ToString();
                }

                conn.Close();
            }
            return test != null;
        }
        
        public bool CheckEmail(string email)
        {
            string emailExists = null;
            
            using(MySqlConnection conn = getConnection())
            {
                string sql = $"SELECT email FROM user WHERE email = '{email}'";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    emailExists = reader["email"].ToString();
                }

                conn.Close();
            }
            return emailExists != null;
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
                    string error = "Username already in use";
                    return error;
                }
                conn.Close();

                return "";
            }

            

            }


    }
}
