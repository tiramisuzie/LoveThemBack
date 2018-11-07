using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace LoveThemBackWebApp.Models
{
  public class PetJSON
  {

    [JsonProperty("@encoding")]
    public string @encoding { get; set; }

    [JsonProperty("@version")]
    public string @version { get; set; }

    [JsonProperty("petfinder")]
    public Petfinder petfinder { get; set; }
  }

  public class Petfinder
  {

    [JsonProperty("@xmlns:xsi")]
    public string @xmlns { get; set; }

    [JsonProperty("lastOffset")]
    public LastOffset lastOffset { get; set; }

    [JsonProperty("pets")]
    public Pets pets { get; set; }

    [JsonProperty("header")]
    public Header header { get; set; }

    [JsonProperty("@xsi:noNamespaceSchemaLocation")]
    public string @xsi { get; set; }
  }

  public class Header
  {

    [JsonProperty("timestamp")]
    public Timestamp timestamp { get; set; }
  }

  public class Pets
  {

    [JsonProperty("pet")]
    public List<Pet> pet { get; set; }
  }

  public class Timestamp
  {

    [JsonProperty("$t")]
    public DateTime ts { get; set; }
  }

  public class Pet
  {

    [JsonProperty("options")]
    public Options options { get; set; }

    [JsonProperty("status")]
    public Status status { get; set; }

    [JsonProperty("contact")]
    public Contact contact { get; set; }

    [JsonProperty("age")]
    public Age age { get; set; }

    [JsonProperty("size")]
    public Size size { get; set; }

    [JsonProperty("media")]
    public Media media { get; set; }

    [JsonProperty("id")]
    public Id id { get; set; }

    [JsonProperty("shelterPetId")]
    public ShelterPetId shelterPetId { get; set; }

    [JsonProperty("breeds")]
    public Breeds breeds { get; set; }

    [JsonProperty("name")]
    public Name name { get; set; }

    [JsonProperty("sex")]
    public Sex sex { get; set; }

    [JsonProperty("description")]
    public Description description { get; set; }

    [JsonProperty("mix")]
    public Mix mix { get; set; }

    [JsonProperty("shelterId")]
    public ShelterId shelterId { get; set; }

    [JsonProperty("lastUpdate")]
    public LastUpdate lastUpdate { get; set; }

    [JsonProperty("animal")]
    public Animal animal { get; set; }

    public List<PetReview> review { get; set; }
  }

  public class LastOffset
  {

    [JsonProperty("$t")]
    public string tsl { get; set; }
  }

  public class Options
  {

    [JsonProperty("option")]
    public object option { get; set; }
  }

  public class Status
  {

    [JsonProperty("$t")]
    public string tse { get; set; }
  }

  public class Phone
  {

    [JsonProperty("$t")]
    public string tsb { get; set; }
  }

  public class State
  {

    [JsonProperty("$t")]
    public string tsp { get; set; }
  }

  public class Address2
  {
  }

  public class Email
  {

    [JsonProperty("$t")]
    public string tsn { get; set; }
  }

  public class City
  {

    [JsonProperty("$t")]
    public string tsb { get; set; }
  }

  public class Zip
  {

    [JsonProperty("$t")]
    public string tsv { get; set; }
  }

  public class Fax
  {
  }

  public class Address1
  {

    [JsonProperty("$t")]
    public string tsa { get; set; }
  }

  public class Contact
  {

    [JsonProperty("phone")]
    public Phone phone { get; set; }

    [JsonProperty("state")]
    public State state { get; set; }

    [JsonProperty("address2")]
    public Address2 address2 { get; set; }

    [JsonProperty("email")]
    public Email email { get; set; }

    [JsonProperty("city")]
    public City city { get; set; }

    [JsonProperty("zip")]
    public Zip zip { get; set; }

    [JsonProperty("fax")]
    public Fax fax { get; set; }

    [JsonProperty("address1")]
    public Address1 address1 { get; set; }
  }

  public class Age
  {

    [JsonProperty("$t")]
    public string tsna { get; set; }
  }

  public class Size
  {

    [JsonProperty("$t")]
    public string tsme { get; set; }
  }

  public class Photo
  {

    [JsonProperty("@size")]
    public string @size { get; set; }

    [JsonProperty("$t")]
    public string tser { get; set; }

    [JsonProperty("@id")]
    public string id { get; set; }
  }

  public class Photos
  {

    [JsonProperty("photo")]
    public IList<Photo> photo { get; set; }
  }

  public class Media
  {

    [JsonProperty("photos")]
    public Photos photos { get; set; }
  }

  public class Id
  {

    [JsonProperty("$t")]
    public string tspo { get; set; }
  }

  public class ShelterPetId
  {

    [JsonProperty("$t")]
    public string tsner { get; set; }
  }

  public class Breeds
  {

    [JsonProperty("breed")]
    public object breed { get; set; }
  }

  public class Name
  {

    [JsonProperty("$t")]
    public string tsju { get; set; }
  }

  public class Sex
  {

    [JsonProperty("$t")]
    public string tsoe { get; set; }
  }

  public class Description
  {

    [JsonProperty("$t")]
    public string tsnve { get; set; }
  }

  public class Mix
  {

    [JsonProperty("$t")]
    public string tsci { get; set; }
  }

  public class ShelterId
  {

    [JsonProperty("$t")]
    public string teqw { get; set; }
  }

  public class LastUpdate
  {

    [JsonProperty("$t")]
    public DateTime rsgh { get; set; }
  }

  public class Animal
  {

    [JsonProperty("$t")]
    public string tsgh { get; set; }
  }
}
