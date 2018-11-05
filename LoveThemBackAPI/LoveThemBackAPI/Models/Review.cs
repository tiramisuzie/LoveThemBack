using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackAPI.Models
{
  public class Review
  {
        public int UserID { get; set; }
        public int PetID { get; set; }

        public string Impression{ get; set; }
        public bool Affectionate { get; set; }
        public bool Friendly { get; set; }
        public bool HighEnergy { get; set; }
        public bool Healthy { get; set; }
        public bool Intelligent { get; set; }
        public bool Cheery { get; set; }
        public bool Playful { get; set; }

        public ICollection<Pet> Pet { get; set; }
    }
}
