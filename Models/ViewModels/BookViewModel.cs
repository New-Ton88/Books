using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public Author Author { get; set; }
        public Language Language { get; set; }
        public Cover Cover { get; set; }
        public Genre Genre { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<Cover> Covers { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
        public string StatusMessage { get; set; }

    }
}
