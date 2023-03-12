using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string Email { get; set; }   = string.Empty; 
        public byte[] PasswordSalt { get; set; }  
        public byte[] PasswordHash { get; set; }    
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Role { get; set; } = "Customer";
    }
}
