﻿using Gogus.Model;
using Gogus.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gogus.Helper
{
    public class WebServiceHelper
    {
        private static WebServiceHelper _instance { get; set; }
        public static WebServiceHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WebServiceHelper();

                return _instance;
            }
        }

        public WebServiceHelper()
        {

        }

        public async Task GetCategories()
        {
            var resultCategories = await HttpRequestHelper.Instance.FillObjectWithJson<TemplateCategoryWebService>(new TemplateCategoryWebService(), "https://api.themoviedb.org/3/genre/movie/list?api_key=d2462a6f978e52644f316b154b5c41ba&language=fr");
            MainPageViewModel.Instance.Categories = resultCategories.genres;
        }

        public async Task GetMovies()
        {
            var movies = await HttpRequestHelper.Instance.FillListObjectWithJson<Movie>(MainPageViewModel.Instance.Movies, "http://localhost:8080/rest/getMovies/Uy2wyWu22R9vzTYpn97E8pWQYK245pp2");

            foreach (var movie in movies)
            {
                try
                {
                    string titleFile = movie.Name.Replace("_FR.avi", "");
                    titleFile = titleFile.Replace("_FR.mkv", "");
                    titleFile = titleFile.Replace("_FR.mp4", "");
                    var result = await HttpRequestHelper.Instance.FillObjectWithJson<TemplateWebService>(new TemplateWebService(), "https://api.themoviedb.org/3/search/movie?api_key=d2462a6f978e52644f316b154b5c41ba&language=fr&query=" + titleFile);

                    if (result.results.Count > 0)
                    {
                        result.results[0].poster_path = "http://image.tmdb.org/t/p/w500" + result.results[0].poster_path;
                        result.results[0].Category = MainPageViewModel.Instance.Categories.Where(x => x.id == result.results[0].genre_ids[0]).FirstOrDefault().name;

                        MainPageViewModel.Instance.Movies.Add(result.results[0]);
                    }

                    else
                    {
                        movie.Category = "Inclassable";
                        MainPageViewModel.Instance.Movies.Add(movie);
                    }
                }
                catch (Exception E)
                {

                }
            }

            return;
        }
    }
}