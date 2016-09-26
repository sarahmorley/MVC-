using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SnippetsHub.Models
{
    public class Snippet
    {
        [Required(ErrorMessage = "You must enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You must enter a Description")]
        public string SnippetDescription { get; set; }

        [Required(ErrorMessage = "You must enter some content")]
        public string Content { get; set; }
   
        public string Email { get; set; }

        public string GroupName { get; set; }


    }
}