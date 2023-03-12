using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(25, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password",ErrorMessage ="Your Confirm Password Does not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
