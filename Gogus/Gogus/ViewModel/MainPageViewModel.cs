using Gogus.Helper;
using Gogus.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Gogus.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public List<Movie> Movies { get; set; }
        public List<Movie> MoviesDB { get; set; }

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

        private List<MovieCategory> _Items;
        public List<MovieCategory> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                this._Items = value;
                this.OnPropertyChanged("Items");
            }
        }

        public Movie CurrentMovie { get; set; }

        public MainPageViewModel()
        {
            Movies = new List<Movie>();
            MoviesDB = new List<Movie>();
        }

        public async Task<Boolean> GetDatas()
        {
            try
            {
                // Call to web service to get all genre
                await WebServiceHelper.Instance.GetCategories();

                // Call to web service class to get information about movies
                await WebServiceHelper.Instance.GetMovies();

                var moviesByCategories = Movies.OrderBy(x => x.title).GroupBy(x => x.Category)
                    .Select(x => new MovieCategory { Title = x.Key, Items = x.ToList() });

                Items = moviesByCategories.OrderBy(x => x.Title).ToList();

                return true;
            }
            catch (Exception E)
            {
                return false;
            }
        }

        #region Event PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
