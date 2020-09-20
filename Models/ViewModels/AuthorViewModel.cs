using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models.ViewModels
{
    public class AuthorViewModel
    {
        public Author Author { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
