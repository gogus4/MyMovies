using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gogus.Model
{
    public class MovieCategory
    {
        public String Title { get; set; }
        public int Count { get { if (Items != null) return Items.Count; else return 0; } }
        public List<Movie> Items { get; set; }
    }
}
