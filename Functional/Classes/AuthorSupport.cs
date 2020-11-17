using Books.Data;
using Books.Functional.Interfaces;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Books.StaticDetails;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Books.Functional.Classes
{
    public class AuthorSupport : IAuthorSupport
    {
        public string WebRootPath { get; set; }
        public ApplicationDbContext Db { get; set; }

        private string DefaultImgCreate(AuthorViewModel authorVM)
        {
            var uploads = Path.Combine(WebRootPath, SD.AuthorsImgPath + SD.DefaultAuthorImgName);
            var newImgPath = SD.AuthorsImgPath + authorVM.Author.Id + ".jpg";
            File.Copy(WebRootPath + uploads, WebRootPath + newImgPath);

            return newImgPath;
        }

        public async ValueTask<List<string>> ImgCreateAsync(AuthorViewModel authorVM, IFormFile file)
        {
            // Files uploaded
            // ------------------------------
            var uploads = Path.Combine(WebRootPath + SD.AuthorsImgPath);
            var extension = Path.GetExtension(file.FileName);

            // Create file for author
            // ------------------------------
            using (var fileStream = new FileStream(Path.Combine(uploads, authorVM.Author.Id + extension), FileMode.Create))
            {
                // Add file added by user into created file
                // ------------------------------
                await file.CopyToAsync(fileStream);
            }

            var fileDetails = new List<string> { uploads, extension };

            return fileDetails;
        }

        public async ValueTask<string> ImageAddAsync(ApplicationDbContext db 
                                        , IFormFileCollection files, AuthorViewModel authorVM)
        {
            var authorFromDb = await db.Author.FindAsync(authorVM.Author.Id);

            if (files.Count() > 0)
            {
                var fileDetails = await ImgCreateAsync(authorVM, files[0]);
                var extension = fileDetails[1];

                // Add img image path into database
                // ------------------------------
                authorFromDb.Image = SD.AuthorsImgPath + authorVM.Author.Id + extension;
            }
            else
            {
                // image wasn't uploaded so create image for current author
                // from default image and save it's path into db
                // ------------------------------
                var newImgPath = DefaultImgCreate(authorVM);

                // Add img image path into database
                // ------------------------------
                authorFromDb.Image = newImgPath;

            }

            // Save Changes to database
            // ------------------------------
            await db.SaveChangesAsync();

            var statusMessage = authorVM.Author.Name + " successfully created";

            return statusMessage;
        }


        public async ValueTask<Tuple<short, string>> AuthorExists(AuthorViewModel authorVM)
        {
            // Check if author name or alias entered already exists
            // ------------------------------
            var authors = Db.Author.Where(a => a.Name == authorVM.Author.Name || a.Alias == authorVM.Author.Alias);
            short exitCode = 0;
            var statusMessage = "";

            // If author already exists inform user
            // ------------------------------
            if (authors.Count() > 0)
            {
                // Verify if user name already exists
                // ------------------------------
                var author = await authors.FirstAsync();
                var authorNameExists = author.Name == authorVM.Author.Name;
                if (authorNameExists)
                {
                    statusMessage = "Error: Author with " + author.Name + " name already exists!";
                    exitCode = 1;
                }

                // Verify if user alias already exists
                // ------------------------------
                if (author.Alias == authorVM.Author.Alias)
                {
                    // If user Name also exist then append in newline message
                    // ------------------------------
                    if (authorNameExists)
                    {
                        statusMessage = "Error: Author with " + author.Name + " name and with " + author.Alias + " alias already exists!";
                        exitCode = 3;
                    }
                    // If author name doesn't exists then 
                    // show only error message for alias
                    // ------------------------------
                    else
                    {
                        statusMessage = "Error: Author with " + author.Alias + " alias already exists!";
                        exitCode = 2;
                    }

                }

            }

            var status = Tuple.Create(exitCode, statusMessage);

            return status;
        
        }

        public async ValueTask<Tuple<short, string>> AuthorCreate(ModelStateDictionary modelState, AuthorViewModel authorVM
                                                                    , HttpContext httpContext)
        {
            short exitCode;
            string statusMessage;

            if (modelState.IsValid)
            {
                (exitCode, statusMessage) = await AuthorExists(authorVM);
            }
            else
            {
                exitCode = -1;
                statusMessage = "modelState is invalid.";
            }
            // If no errors found add author into database and create new image
            // ------------------------------
            if (exitCode == 0)
            {
                await Db.Author.AddAsync(authorVM.Author);
                await Db.SaveChangesAsync();

                statusMessage = await ImageAddAsync(Db, httpContext.Request.Form.Files, authorVM);
            }

            // If something fails go back to the same view
            // ------------------------------
            authorVM.Genres = Db.Genre;
            authorVM.Languages = Db.Language;
            authorVM.StatusMessage = statusMessage;

            // Create Tupple to return execution status to caller
            // ------------------------------
            var status = Tuple.Create(exitCode, statusMessage);
            return status;
        }

    }
}
