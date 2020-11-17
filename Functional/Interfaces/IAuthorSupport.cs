using Books.Data;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Functional.Interfaces
{
    public interface IAuthorSupport
    {
        ValueTask<string> ImageAddAsync(ApplicationDbContext db
                            , IFormFileCollection files, AuthorViewModel authorVM);

        ValueTask<List<string>> ImgCreateAsync(AuthorViewModel authorVM, IFormFile file);
        ValueTask<Tuple<short, string>> AuthorExists(AuthorViewModel authorVM);
        ValueTask<Tuple<short, string>> AuthorCreate(ModelStateDictionary modelState, AuthorViewModel authorVM, HttpContext httpContext);
    }
}
