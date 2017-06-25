using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogA.Models.Identity
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Invalid Confirm")]
        public string ConfirmPassword { get; set; }

    }
}

