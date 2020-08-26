using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Cover
    {
        // Set primary Key as TinyInt in database
        // -----------------------------
        [Key]
        public Int16 Id { get; set; }

        // Set Name not nullable with length 30
        // -----------------------------
        [Required]
        [Column(TypeName ="nvarchar(30)")]
        public string Name { get; set; }
    }
}
