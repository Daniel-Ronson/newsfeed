using MySql.Data.MySqlClient;
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

        private List<String> getGenres(int articleId)
        {
            List<String> genres = new List<String>();

            using (MySqlConnection con = getConnection())
            {
                MySqlCommand cmd = new MySqlCommand("getGenres", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@articleId", articleId);
                cmd.Parameters["@articleId"].Direction = ParameterDirection.Input;

                con.Open(); //open db connection

                MySqlDataReader rdr = cmd.ExecuteReader();

                //while loop
                while (rdr.Read())
                {
                    genres.Add(rdr["genre"].ToString());
                }

                con.Close();
            }
            return genres;
        }

        public Tuple<List<Article>,List<string>> ListArticles(int websiteId=1)
        {
            
            List<Article> LArticle = new List<Article>();
            List<string> genreids = new List<string>();

            using (MySqlConnection con = getConnection())
            {
                MySqlCommand cmd = new MySqlCommand("getArticles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@websiteId", websiteId);
                cmd.Parameters["@websiteId"].Direction = ParameterDirection.Input;
                con.Open(); //open db connection

                MySqlDataReader rdr = cmd.ExecuteReader();

                //while loop
                while (rdr.Read())
                {
                    Article w = new Article();
                    w.ID = Convert.ToInt32(rdr["articleid"]);
                    w.Title = rdr["title"].ToString();
                    w.WebsiteUrl = rdr["websiteUrl"].ToString();
                    w.PublisherUrl = rdr["publisherUrl"].ToString();
                    w.Date = rdr["date"].ToString();
                    w.WebsiteName = rdr["websitename"].ToString();
                    w.Genres = getGenres(w.ID);
                    w.Description = rdr["description"].ToString();

                    LArticle.Add(w);

                    //(genres in CNN...) keep track of the genres that are in the entire website 
                    if (!genreids.Contains(w.Genres[0])) {
                        genreids.Add(w.Genres[0]);
                    }                    
                }

                con.Close();
            }
            return Tuple.Create(LArticle,genreids);
        }
    }
}
