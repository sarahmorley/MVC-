using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SnippetsHub.Models
{
    public class Group
    {

        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group Name is missing")]
        public string GroupName { get; set; }


        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is missing")]
        public string GroupDescription { get; set; }

       
    }
}