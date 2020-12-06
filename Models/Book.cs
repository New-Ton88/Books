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
        [Display(Name= "Author01")]
        public short AuthorId01 { get; set; }

        [ForeignKey("AuthorId01")]
        public virtual Author Author01 { get; set; }

        [Display(Name = "Author02")]
        public short AuthorId02 { get; set; }

        [ForeignKey("AuthorId02")]
        public virtual Author Author02 { get; set; }

        [Display(Name = "Author03")]
        public short AuthorId03 { get; set; }

        [ForeignKey("AuthorId03")]
        public virtual Author Author03 { get; set; }

        [Display(Name = "Author04")]
        public short AuthorId04 { get; set; }

        [ForeignKey("AuthorId04")]
        public virtual Author Author04 { get; set; }

        [Display(Name = "Author05")]
        public short AuthorId05 { get; set; }

        [ForeignKey("AuthorId05")]
        public virtual Author Author05 { get; set; }

        [Column(TypeName ="Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Cover")]
        public short CoverId { get; set; }

        [ForeignKey("CoverId")]
        public virtual Cover Cover { get; set; }

        [Required]
        [Display(Name = "Genre01")]
        public short GenreId01 { get; set; }

        [ForeignKey("GenreId01")]
        public virtual Genre Genre01 { get; set; }

        [Display(Name = "Genre02")]
        public short GenreId02 { get; set; }

        [ForeignKey("GenreId02")]
        public virtual Genre Genre02 { get; set; }

        [Display(Name = "Genre03")]
        public short GenreId03 { get; set; }

        [ForeignKey("GenreId03")]
        public virtual Genre Genre03 { get; set; }

        [Display(Name = "Genre04")]
        public short GenreId04 { get; set; }

        [ForeignKey("GenreId04")]
        public virtual Genre Genre04 { get; set; }

        [Display(Name = "Genre05")]
        public short GenreId05 { get; set; }

        [ForeignKey("GenreId05")]
        public virtual Genre Genre05 { get; set; }

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

        [Range(1, int.MaxValue, ErrorMessage = "Price should be greater than 1$.")]
        public double Price { get; set; }

    }
}
