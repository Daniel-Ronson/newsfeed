using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class LoginRegisterForm
    {
        [DisplayName("Username / Email")]
        public string LoginUserEmail { get; set; }
        [DisplayName("Password")]
        public string LoginPassword { get; set; }
        [DisplayName("Email")]
        public string RegisterEmail { get; set; }
        [DisplayName("Username")]
        public string RegisterUsername { get; set; }
        [DisplayName("Password")]
        public string RegisterPassword { get; set; }
        [DisplayName("Repeat password")]
        public string RegisterPasswordCheck { get; set; }
    }
}
