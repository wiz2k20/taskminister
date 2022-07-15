using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;

using taskminister.musik.Entity;
using taskminister.musik.Interface;
using taskminister.security.Database;


namespace taskminister.musik.Repository
{
    public class RepoMusik : IRepoMusik
    {
        public IConnection connect;

        public RepoMusik(IConnection _connect) {
            connect = _connect;
        }

        public List<Information> SQLPlaylist()
        {
            using (MySqlConnection c = connect.keeper())
            {
                c.Open();
                var d = c.CreateCommand();
                d.CommandText = RepoSQL.MusikSelectAsc();

                var reader = d.ExecuteReader();
                var dbresult = new List<Information>();
                while (reader.Read())
                {
                    dbresult.Add(new Information()
                    {
                        Control = Convert.ToInt32(reader["dbcontrol"]),
                        Name = reader["dbname"].ToString(),
                        Artist = reader["dbartist"].ToString(),
                        Url = reader["dburl"].ToString(),
                        Cover = reader["dbcover"].ToString(),
                    });
                }
                c.Close();
                return dbresult;
            }
        }

        //public bool SQLPlaylistInsert(string query, string songName, string songArtist, string songUrl, string songCover)
        //{
        //    using (var c = new MySqlConnection(this.Configuration.GetConnectionString("NewCon")))
        //    {
        //        c.Open();
        //        using var d = c.CreateCommand();
        //        d.Parameters.AddWithValue("@songName", songName);
        //        d.Parameters.AddWithValue("@songArtist", songArtist);
        //        d.Parameters.AddWithValue("@songUrl", songUrl);
        //        d.Parameters.AddWithValue("@songCover", songCover);
        //        d.CommandText = query;

        //        var resultQuery = d.ExecuteNonQuery();
        //        if (resultQuery > 0) { return true; }
        //        else { return false; }
        //    }
        //}


        //public List<MusikModelView> GetPlaylistInfo()
        //{
        //    var queryPlayInfo = RepoSQL.PlaylistInfo();
        //    var result = SQLPlaylist(queryPlayInfo);

        //    return result;
        //}
    }
}
