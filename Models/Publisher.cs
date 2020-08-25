using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Publisher
    {
        // Primary Key casted to database as smallInt
        // ----------------------------------
        [Key]
        public Int16 Id { get; set; }

        // Set field as not Nullable and max length to 40
        // ----------------------------------
        [Required]
        [Column(TypeName ="nvarchar(40)")]
        public string Name { get; set; }
    }
}
