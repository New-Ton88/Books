using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Author
    {
        [Key]
        public short Id { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string Alias { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string Birthday { get; set; }

        [Display(Name="Language")]
        public short LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }

        public string Description { get; set; }

        [Column(TypeName="nvarchar(50)")]
        public string Image { get; set; }

        // Foreign Keys to genres
        // ------------------
        [Display(Name="Genre01")]
        public short? GenreId01 { get; set; }

        [ForeignKey("GenreId01")]
        public virtual Genre Genre01 { get; set; }

        [Display(Name = "Genre02")]
        public short? GenreId02 { get; set; }

        [ForeignKey("GenreId02")]
        public virtual Genre Genre02 { get; set; }

        [Display(Name = "Genre03")]
        public short? GenreId03 { get; set; }

        [ForeignKey("GenreId03")]
        public virtual Genre Genre03 { get; set; }

        [Display(Name = "Genre04")]
        public short? GenreId04 { get; set; }

        [ForeignKey("GenreId04")]
        public virtual Genre Genre04 { get; set; }

        [Display(Name = "Genre05")]
        public short? GenreId05 { get; set; }

        [ForeignKey("GenreId05")]
        public virtual Genre Genre05 { get; set; }

        [Display(Name = "Genre06")]
        public short? GenreId06 { get; set; }

        [ForeignKey("GenreId06")]
        public virtual Genre Genre06 { get; set; }

        [Display(Name = "Genre07")]
        public short? GenreId07 { get; set; }

        [ForeignKey("GenreId07")]
        public virtual Genre Genre07 { get; set; }

    }
}
