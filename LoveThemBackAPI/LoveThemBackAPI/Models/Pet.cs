using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackAPI.Models
{
  public class Pet
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PetID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string Description { get; set; }
    public Review Review { get; set; }
  }

  public enum Sex
  {
    [System.ComponentModel.Description("Male")]
    Male = 0,
    [System.ComponentModel.Description("Female")]
    Female = 1
  }
}
