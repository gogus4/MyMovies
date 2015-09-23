using Gogus.Helper;
using Gogus.Model;
using Gogus.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Gogus.View
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = MainPageViewModel.Instance;
        }

        private void ActionShowMenu_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void itemGridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainPageViewModel.Instance.CurrentMovie = itemGridView.SelectedItem as Movie;
            this.Frame.Navigate(typeof(ViewMovie));
        }

        private async void Refresh_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainPageViewModel.Instance.Movies = new List<Movie>();
            await MainPageViewModel.Instance.GetDatas();
        }

        private void ActionSearchMovie_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (SearchMovie.Visibility == Visibility.Visible)
                SearchMovie.Visibility = Visibility.Collapsed;

            else
                SearchMovie.Visibility = Visibility.Visible;
        }

        private void SearchMovie_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SearchMovie.Visibility = Visibility.Collapsed;
        }

        private void SearchMovie_SuggestionsRequested(SearchBox sender, SearchBoxSuggestionsRequestedEventArgs args)
        {
            string queryText = args.QueryText;
            if (!string.IsNullOrEmpty(queryText))
            {
                Windows.ApplicationModel.Search.SearchSuggestionCollection suggestionCollection = args.Request.SearchSuggestionCollection;
                foreach (var movieSuggestion in MainPageViewModel.Instance.Movies)
                {
                    if (movieSuggestion.Name.StartsWith(queryText, StringComparison.CurrentCultureIgnoreCase))
                    {
                        suggestionCollection.AppendQuerySuggestion(movieSuggestion.TitleToDisplay);
                    }
                }
            }
        }
    }
}
