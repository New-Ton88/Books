using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Category
    {
        // Create Key of smallint type
        // ----------------------------
        [Key]
        public short Id { get; set; }

        // Create Name of nvarchar(50) non-nullable type
        // ----------------------------
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
    }
}
