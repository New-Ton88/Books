using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Book
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Column(TypeName="nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Author")]
        public short AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }

        [Column(TypeName ="Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Cover")]
        public short CoverId { get; set; }

        [ForeignKey("CoverId")]
        public virtual Cover Cover { get; set; }

        [Display(Name = "Genre")]
        public short GenreId { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        [Display(Name = "Publisher")]
        public short PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; }

        [Display(Name = "Language")]
        public short LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string Image { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public short OnStock { get; set; }

        [Required]
        public string Type { get; set; }

        public enum EType { Book = 0, AudioBook = 1 }

        [Range(1, int.MaxValue, ErrorMessage = "Prize should be greater than 1$.")]
        public double Price { get; set; }

    }
}
