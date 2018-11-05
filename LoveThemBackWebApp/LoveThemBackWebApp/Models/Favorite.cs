using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Models
{
    public class Favorite
    {
        public int UserID { get; set; }
        public int PetID { get; set; }
        public string Notes { get; set; }
    }
}
