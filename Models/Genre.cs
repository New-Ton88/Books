using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Genre
    {
        // Primary Key casted to database as smallInt
        // ----------------------------------
        [Key]
        public Int16 Id { get; set; }

        // Set field as not nullable and max length to 30
        // ----------------------------------
        [Required]
        [Column(TypeName ="nvarchar(30)")]
        public string Name { get; set; }

        // Foreign Key to Category
        // ----------------------------------
        [Required]
        [Display(Name ="Category")]
        public short CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
