﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;


namespace News.Models
{
    public class ArticleContext : DbContext
    {
        public string ConnectionString { get; set; }
        public DbSet<Article> Articles { get; set; }

        public ArticleContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection getConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Article> ListArticles()
        {
            List<Article> LArticle = new List<Article>();

            using (MySqlConnection con = getConnection())
            {
                MySqlCommand cmd = new MySqlCommand("getArticles", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open(); //open db connection

                MySqlDataReader rdr = cmd.ExecuteReader();

                //while loop
                while (rdr.Read())
                {
                    Article w = new Article();
                    w.ID = Convert.ToInt32(rdr["articleid"]);
                    w.Title = rdr["title"].ToString();
                    w.Url = rdr["url"].ToString();
                    w.Date = rdr["date"].ToString();
                    w.WebsiteId = rdr["websiteid"].ToString();
                    w.Genre = rdr["genreid"].ToString();

                    LArticle.Add(w);
                }

                con.Close();
            }
            return LArticle;
        }
    }
}
