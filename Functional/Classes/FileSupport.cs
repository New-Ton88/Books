using Books.Functional.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Functional.Classes
{
    public class FileSupport : IFileSupport
    {
        public string WebRootPath { get; set; }

        public string DefaultImgCreate(string imagePath, string imageName, short id)
        {
            var uploads = Path.Combine(WebRootPath, imagePath + imageName);
            var newImgPath = imagePath + id + ".jpg";
            File.Copy(WebRootPath + uploads, WebRootPath + newImgPath);

            return newImgPath;
        }

        public async ValueTask<List<string>> ImgCreateAsync(string imagePath, short id, IFormFile file)
        {
            // Files uploaded
            // ------------------------------
            var uploads = Path.Combine(WebRootPath + imagePath);
            var extension = Path.GetExtension(file.FileName);

            // Create file for author
            // ------------------------------
            using (var fileStream = new FileStream(Path.Combine(uploads, id + extension), FileMode.Create))
            {
                // Add file added by user into created file
                // ------------------------------
                await file.CopyToAsync(fileStream);
            }

            var fileDetails = new List<string> { uploads, extension };

            return fileDetails;
        }
    }
}
