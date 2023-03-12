using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Column(TypeName ="Decimal(18,2)")]
        public decimal Price { get; set; }  
        public int CategoryId { get; set; }
    }
}
