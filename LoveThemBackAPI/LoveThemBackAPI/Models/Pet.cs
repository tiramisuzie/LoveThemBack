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
    public string Animal { get; set; }
    public string Breed { get; set; }
    public string Mix { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Sex { get; set; }
    public string Size { get; set; }
    public string Description { get; set; }
    public string ShelterID { get; set; }
    public string ShelterName { get; set; }
    public string Photos { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Zip { get; set; }
    public string State { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public List<Review> Reviews { get; set; }
  }
}
