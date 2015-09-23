using Gogus.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Gogus.Model
{
    public class Movie
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Category { get; set; }

        public int ColumnSpan { get; set; }
        public int RowSpan { get; set; }

        public MovieTemplate Template { get; set; }

        // Web Service Variable
        public string original_title { get; set; }
        public string overview { get; set; }
        public string adult { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string backdrop_path { get; set; }

        [Ignore]
        public List<string> genre_ids { get; set; }

        // Using for SQLite
        public string genre { get; set; }

        public string poster_path { get; set; }
        public string vote_average { get; set; }

        // Refactoring
        public string TitleToDisplay { get; set; }

        [Ignore]
        public BitmapImage Poster { get; set; }

        public async Task SetSource(StorageFile file)
        {
            FileRandomAccessStream stream = (FileRandomAccessStream)await file.OpenAsync(FileAccessMode.Read);
            Poster.SetSource(stream);
        }

        public Movie()
        {
            init();
        }

        private void init()
        {
            ColumnSpan = 1;
            RowSpan = 1;

            Poster = new BitmapImage();
        }
    }
}