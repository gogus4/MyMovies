using Gogus.Helper;
using Gogus.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gogus.ViewModel
{
    public class MainPageViewModel
    {
        public List<Movie> Movies { get; set; }
        public List<Category> Categories { get; set; }

        private static MainPageViewModel _instance { get; set; }
        public static MainPageViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainPageViewModel();

                return _instance;
            }
        }

        public List<MovieCategory> Items { get; set; }

        public MainPageViewModel()
        {
            Movies = new List<Movie>();

            /*Movies = new List<Movie>
                             {
                                 new Movie {Name = "The Ewok Adventure", Category = "Adventure", ColumnSpan=2, RowSpan=2,Template=MovieTemplate.Large },
                                 new Movie {Name = "The In-Laws", Category = "Adventure" ,Template = MovieTemplate.Medium},
                                 new Movie {Name = "The Man Called Flintstone", Category = "Adventure",Template = MovieTemplate.Medium},
                                 new Movie {Name = "The Man Called Flintstone", Category = "Adventure",Template = MovieTemplate.Medium},
                                 new Movie {Name = "The Man Called Flintstone", Category = "Adventure",Template = MovieTemplate.Medium},
                                 new Movie {Name = "The Man Called Flintstone", Category = "Adventure",Template = MovieTemplate.Medium},

                                 new Movie {Name = "Super Fuzz", Category = "Comedy",ColumnSpan=2, RowSpan=2,Template = MovieTemplate.Large},
                                 new Movie {Name = "The Knock Out Cop", Category = "Comedy",Template = MovieTemplate.Medium},
                                 new Movie {Name = "Best Worst Movie", Category = "Comedy",Template = MovieTemplate.Medium},

                                 new Movie {Name = "The Last Unicorn", Category = "Fantasy",Template = MovieTemplate.Medium},
                                 new Movie {Name = "Blithe Spirit", Category = "Fantasy",Template = MovieTemplate.Medium},
                                 new Movie {Name = "Here Comes Mr. Jordan", Category = "Fantasy",Template = MovieTemplate.Medium},

                                 new Movie {Name = "The Last Unicorn", Category = "Fantasy1",Template=MovieTemplate.Medium},
                                 new Movie {Name = "Blithe Spirit", Category = "Fantasy3",Template=MovieTemplate.Medium},
                                 new Movie {Name = "Here Comes Mr. Jordan", Category = "Fantasy2",Template=MovieTemplate.Medium},
                             };

            var moviesByCategories = movies.GroupBy(x => x.Category)
                .Select(x => new MovieCategory { Title = x.Key, Items = x.ToList() });

            Items = moviesByCategories.ToList();*/
        }

        public async Task GetDatas()
        {
            // Call to web service to get all genre
            await WebServiceHelper.Instance.GetCategories();

            // Call to web service class to get information about movies
            await WebServiceHelper.Instance.GetMovies();

            var moviesByCategories = Movies.OrderBy(x=> x.title).GroupBy(x => x.Category)
                .Select(x => new MovieCategory { Title = x.Key, Items = x.ToList() });

            Items = moviesByCategories.OrderBy(x => x.Title).ToList();
        }
    }
}
