using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class GenreContext
    {
        public string ConnectionString { get; set; }

        public GenreContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection getConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        //pass list of known genres for a given website to this function, so we can print out the relevant genres 
        public List<Genre> ListGenres(List<string> genreids)

        {
            List<Genre> GenreList = new List<Genre>();

            using (MySqlConnection conn = getConnection())
            {
                string sql = "SELECT * from genre";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())

                {

                    Genre g = new Genre();
                    g.genreID = Convert.ToInt32(reader["genreid"]);
                    g.genre = reader["genre"].ToString();


                    if (genreids.Contains(g.genre))
                    {
                        GenreList.Add(g);
                    }

                }

                conn.Close();
            }
            return GenreList;
        }
    }
}
