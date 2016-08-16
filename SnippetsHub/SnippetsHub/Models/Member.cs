using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnippetsHub.Models
{
    public class Member
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}