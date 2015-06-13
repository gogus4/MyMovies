using Gogus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Gogus.Common
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LargeMovieTemplate { get; set; }
        public DataTemplate MediumMovieTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item,
                                                           DependencyObject container)
        {
            var movie = (Movie)item;

            switch (movie.Template)
            {
                case MovieTemplate.Large:
                    return LargeMovieTemplate;

                case MovieTemplate.Medium:
                    return MediumMovieTemplate;

                case MovieTemplate.Small:
                    return MediumMovieTemplate;

                default:
                    return MediumMovieTemplate;
            }
        }
    }
}
