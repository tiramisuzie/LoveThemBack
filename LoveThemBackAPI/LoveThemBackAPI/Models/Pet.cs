using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LoveThemBackAPI.Models
{
  public class Pet
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PetID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Sex { get; set; }
    public string Description { get; set; }
    public List<Review> Reviews { get; set; }
  }
}
