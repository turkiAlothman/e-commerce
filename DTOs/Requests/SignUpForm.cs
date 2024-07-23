using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.DTOs.Requests
{
    public class SignUpForm
    {
        [Required]
        [StringLength(30)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(40, MinimumLength = 8)]
        public string Password { get; set; }
        
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        
        [Required]
        [Compare("Password")]
        public string PasswordConfirm {set; get;}
    }
}