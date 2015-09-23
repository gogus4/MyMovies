using Gogus.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Gogus.Helper
{
    public class SQLiteHelper
    {
        private static SQLiteHelper _instance { get; set; }
        public static SQLiteHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SQLiteHelper();

                return _instance;
            }
        }

        public String DBPath { get; set; }

        public SQLiteHelper()
        {
            DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.db3");
        }

        public async Task CreateTableMovieDB()
        {
            try
            {
                using (var db = new SQLite.SQLiteConnection(DBPath))
                {
                    db.CreateTable<Movie>();
                    db.Commit();

                    db.Dispose();
                    db.Close();
                }
            }
            catch (Exception E)
            {

            }
        }

        public async void InsertMovieDB(Movie movie)
        {
            try
            {
                using (var db = new SQLite.SQLiteConnection(DBPath))
                {
                    db.Insert(new Movie()
                    {
                        adult = movie.adult,
                        backdrop_path = movie.backdrop_path,
                        Category = movie.Category,
                        ColumnSpan = movie.ColumnSpan,
                        genre_ids = movie.genre_ids,
                        id = movie.id,
                        Name = movie.Name,
                        original_title = movie.original_title,
                        overview = movie.overview,
                        Path = movie.Path,
                        poster_path = movie.poster_path,
                        RowSpan = movie.RowSpan,
                        Template = movie.Template,
                        title = movie.title,
                        vote_average = movie.vote_average,
                        TitleToDisplay = movie.TitleToDisplay
                    }
                    );

                    db.Commit();
                    db.Dispose();
                    db.Close();
                }
            }
            catch (Exception E)
            {

            }
        }

        public void DeleteMoviesDB()
        {
            using (var db = new SQLite.SQLiteConnection(DBPath))
            {
                db.DeleteAll<Movie>();
            }
        }

        public async Task<List<Movie>> GetMoviesDB()
        {
            try
            {
                var movies = new List<Movie>();

                using (var db = new SQLite.SQLiteConnection(DBPath))
                {
                    var d = from x in db.Table<Movie>() select x;
                    foreach (var sd in d)
                    {
                        string titleFile = sd.Name.Replace("_FR.avi", "").Replace("_FR.mkv", "").Replace("_FR.mp4", "");

                        try
                        {
                            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(titleFile + ".jpg");
                            await sd.SetSource(file);
                        }
                        catch (Exception E)
                        {

                        }

                        movies.Add(sd);
                    }

                    db.Dispose();
                    db.Close();
                }

                return movies;
            }
            catch (Exception E)
            {
            }

            return null;
        }
    }
}
