using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class LoginForm
    {
        [DisplayName("Username / Email")]
        public string userEmail { get; set; }
        [DisplayName("Password")]
        public string password { get; set; }

    }
}
