using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Functional.Interfaces
{
    public interface IFileSupport
    {
        string DefaultImgCreate(string imagePath, string imageName, short id);
        ValueTask<List<string>> ImgCreateAsync(string imagePath, short id, IFormFile file);
    }
}
