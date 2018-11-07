using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoveThemBackWebApp.Models
{
  public class PetPost
  {
    [JsonProperty("petID")]
    public int PetID { get; set; }
    [JsonProperty("animal")]
    public string Animal { get; set; }
    [JsonProperty("breed")]
    public string Breed { get; set; }
    [JsonProperty("mix")]
    public string Mix { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("age")]
    public string Age { get; set; }
    [JsonProperty("sex")]
    public string Sex { get; set; }
    [JsonProperty("size")]
    public string Size { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("shelterID")]
    public string ShelterID { get; set; }
    [JsonProperty("shelterName")]
    public string ShelterName { get; set; }
    [JsonProperty("photos")]
    public string Photos { get; set; }
    [JsonProperty("address")]
    public string Address { get; set; }
    [JsonProperty("city")]
    public string City { get; set; }
    [JsonProperty("zip")]
    public string Zip { get; set; }
    [JsonProperty("state")]
    public string State { get; set; }
    [JsonProperty("phone")]
    public string Phone { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("reviews")]
    public List<PetReview> Review { get; set; }
  }
}
