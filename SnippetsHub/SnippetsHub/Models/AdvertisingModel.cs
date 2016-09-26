using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SnippetsHub.Models
{
    public class AdvertisingModel
    {
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Please tell us your Company name")]
        public string CompanyName { get; set; }

        [Display(Name = "Company Contact Name")]
        [Required(ErrorMessage = "Please tell us your name")]
        public string CompanyContactPerson { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is missing")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string ContactPersonEmail { get; set; }

        [Display(Name = "Advertising Idea")]
        [Required(ErrorMessage = "Please tell us your advertising idea")]
        public string AdvertisingIdea { get; set; }


    }
}