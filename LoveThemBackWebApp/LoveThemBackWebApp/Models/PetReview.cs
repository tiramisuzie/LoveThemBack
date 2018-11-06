using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace LoveThemBackWebApp.Models
{
  public class PetReview
  {
    [JsonProperty("userID")]
    public int UserID { get; set; }
    [JsonProperty("petID")]
    public int PetID { get; set; }
    [JsonProperty("impression")]
    public string Impression { get; set; }
    [JsonProperty("affectionate")]
    public bool Affectionate { get; set; }
    [JsonProperty("friendly")]
    public bool Friendly { get; set; }
    [JsonProperty("highEnergy")]
    public bool HighEnergy { get; set; }
    [JsonProperty("healthy")]
    public bool Healthy { get; set; }
    [JsonProperty("intelligent")]
    public bool Intelligent { get; set; }
    [JsonProperty("cheery")]
    public bool Cheery { get; set; }
    [JsonProperty("playful")]
    public bool Playful { get; set; }
  }
}
