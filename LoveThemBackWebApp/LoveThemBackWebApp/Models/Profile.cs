using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Models
{
    public class Profile
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int LocationZip { get; set; }

        public ICollection<Favorite> Favorites { get; set; }
    }
}
