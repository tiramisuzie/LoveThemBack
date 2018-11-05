using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Models
{
    public class Profile
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int LocationZip { get; set; }
    }
}
