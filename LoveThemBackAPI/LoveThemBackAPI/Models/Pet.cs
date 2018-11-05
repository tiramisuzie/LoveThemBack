using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackAPI.Models
{
  public class Pet
  {
    public int PetID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string Description { get; set; }
    public Review Review { get; set; }
  }

  public enum Sex
  {
    Male,
    Female
  }
}
