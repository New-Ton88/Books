using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Language
    {
        [Key]
        public short Id { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(30)")]
        public string Name { get; set; }
    }
}
