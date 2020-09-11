using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models.ViewModels
{
    public class CategoryAndGenreViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Genre Genre { get; set; }
        public List<string> GenresList { get; set; }
        public string StatusMessage { get; set; }
    }
}
