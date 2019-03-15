using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSecurity.Models
{
    public class Registration
    {
        public int? RegistrationId { get; set; }

        [Required(ErrorMessage = "Enter FirstName")]
        [RegularExpression("[a-zA-Z ]+$",ErrorMessage = "Invalid characters")]
        [StringLength(50, ErrorMessage = "Only 50 Characters are Allowed")]
        public string FirstName { get; set; }

        [RegularExpression("[a-zA-Z ]+$", ErrorMessage = "Invalid characters")]
        [StringLength(50, ErrorMessage = "Only 50 Characters are Allowed")]
        [Required(ErrorMessage = "Enter LastName")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Enter EmailId")]
        public string EmailId { get; set; }

        [RegularExpression("[a-zA-Z ]+$", ErrorMessage = "Invalid characters")]
        [Required(ErrorMessage = "Enter Username")]    
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Enter ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }


        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? Status { get; set; }
    }
}
