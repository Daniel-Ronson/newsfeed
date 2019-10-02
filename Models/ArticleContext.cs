using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class ArticleContext
    {
        public string ConnectionString { get; set; }

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
                string sql = "SELECT * from article";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                //cmd.CommandType = CommandType.StoredProcedure;

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
