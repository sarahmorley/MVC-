using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SnippetsHub.Models
{
    public class Member
    {

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is missing")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Only letters are allowed")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is missing")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is missing")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is missing")]
        [StringLength(10, ErrorMessage = "Password must contain 6 to 10 characters",
            MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is missing")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Username is required")]
        public string Username { get; set; }

    }
}