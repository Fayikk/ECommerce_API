using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserMoney
    {
        [Key]
        public int Id { get; set; } 
        public int UserId { get; set; }
        [Column(TypeName =("Decimal(18,2)"))]
        public decimal Money { get; set; }  
    }
}
