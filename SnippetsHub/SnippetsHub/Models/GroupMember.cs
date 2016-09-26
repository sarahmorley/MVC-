using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SnippetsHub.Models
{
    public class GroupMember
    {
        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group Name is missing")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "Email is missing")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }
    }
}