using LoveThemBackWebApp.Data;
using LoveThemBackWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using LoveThemBackWebApp.Models.Interfaces;
using LoveThemBackWebApp.Controllers;
using LoveThemBackWebApp.Models.Services;
using System.Linq;

namespace LoveThemBackTest
{
  public class UnitTest1
  {
    /// <summary>
    /// Tests Favorite notes
    /// </summary>
    [Fact]
    public void CanGetNotesFromFavoriteModelTest()
    {
      Favorite fav = new Favorite();
      fav.Notes = "This is a note.";

      Assert.Equal("This is a note.", fav.Notes);
    }

    /// <summary>
    /// Tests the ability to change notes on favorites
    /// </summary>
    [Fact]
    public void CanUpdateNotesOnFavoriteTest()
    {
      Favorite fav = new Favorite();
      fav.Notes = "blahblah";

      fav.Notes = "blahblahblah";

      Assert.Equal("blahblahblah", fav.Notes);
    }


    /// <summary>
    /// Tests can set review on animal
    /// </summary>
    [Fact]
    public void CanGetReviewTest()
    {
      Reviews rev = new Reviews();
      rev.Impression = "Good boy";

      Assert.Equal("Good boy", rev.Impression);
    }

    /// <summary>
    /// Tests can change review on animal
    /// </summary>
    [Fact]
    public void CanChangeReviewTest()
    {
      Reviews rev = new Reviews();
      rev.Impression = "Good boy";

      rev.Impression = "Good girl";

      Assert.Equal("Good girl", rev.Impression);
    }

    /// <summary>
    /// Tests can change review on animal
    /// </summary>
    [Fact]
    public void CanReturBoolPropertyOnReviewsTest()
    {
      Reviews rev = new Reviews();
      rev.Playful = true;

      Assert.True(rev.Playful);
    }

    /// <summary>
    /// Tests can change review on animal
    /// </summary>
    [Fact]
    public void CanChangeBoolPropertyReviewTest()
    {
      Reviews rev = new Reviews();
      rev.Healthy = true;

      rev.Healthy = false;

      Assert.False(rev.Healthy);
    }

    /// <summary>
    /// Tests Favorite notes
    /// </summary>
    [Fact]
    public void CanCreateProfileAndReturnUsernameTest()
    {
      Profile profile = new Profile();
      profile.Username = "Banul Beleon";

      Assert.Equal("Banul Beleon", profile.Username);
    }


    /// <summary>
    /// Tests Favorite notes
    /// </summary>
    [Fact]
    public void CanChangeUsernameOnProfileTest()
    {
      Profile profile = new Profile();
      profile.Username = "Banul Beleon";

      profile.Username = "Boozie Bu";

      Assert.Equal("Boozie Bu", profile.Username);
    }
    /// <summary>
    /// tests database operations for Profiles Table
    /// </summary>
    [Fact]
    public async void CRUDProfileTest()
    {
      DbContextOptions<LTBDBContext> options =
          new DbContextOptionsBuilder<LTBDBContext>()
          .UseInMemoryDatabase("Profile")
          .Options;

      using (LTBDBContext context = new LTBDBContext(options))
      {

        //CREATE
        Profile profile = new Profile();
        profile.Username = "Greg";
        profile.LocationZip = 98107;

        context.Profiles.Add(profile);
        context.SaveChanges();

        //READ
        var newProfile = await context.Profiles.FirstOrDefaultAsync(r => r.Username == profile.Username);
        Assert.Equal("Greg", newProfile.Username);

        //UPDATE
        newProfile.Username = "Carl";
        context.Update(newProfile);
        context.SaveChanges();

        var newProf = await context.Profiles.FirstOrDefaultAsync(r => r.Username == newProfile.Username);
        Assert.Equal("Carl", newProf.Username);

        //DELETE
        context.Profiles.Remove(newProf);
        context.SaveChanges();

        var deletedProfile = await context.Profiles.FirstOrDefaultAsync(r => r.Username == newProf.Username);
        Assert.True(deletedProfile == null);
      }
    }
    /// <summary>
    /// tests CRUD operations on database for Favorites Table
    /// </summary>
    [Fact]
    public async void CRUDFavoritesTest()
    {
      DbContextOptions<LTBDBContext> options =
          new DbContextOptionsBuilder<LTBDBContext>()
          .UseInMemoryDatabase("Favorites")
          .Options;

      using (LTBDBContext context = new LTBDBContext(options))
      {

        //CREATE
        Favorite fav = new Favorite();
        Profile profile = new Profile();
        profile.Username = "Greg";
        profile.LocationZip = 98107;
        fav.Profile = profile;
        fav.Notes = "G'BOY GREGG";

        context.Favorites.Add(fav);
        context.SaveChanges();

        //READ
        var newFav = await context.Favorites.FirstOrDefaultAsync(r => r.Profile == fav.Profile);
        Assert.Equal("Greg", fav.Profile.Username);

        //UPDATE
        newFav.Notes = "BAD BOY GREG! You're Bad!";
        context.Update(newFav);
        context.SaveChanges();

        var newFavorite = await context.Favorites.FirstOrDefaultAsync(r => r.Notes == newFav.Notes);
        Assert.Equal("BAD BOY GREG! You're Bad!", newFavorite.Notes);

        //DELETE
        context.Favorites.Remove(newFavorite);
        context.SaveChanges();

        var deletedFavorite = await context.Favorites.FirstOrDefaultAsync(r => r.Profile.Username == newFavorite.Profile.Username);
        Assert.True(deletedFavorite == null);
      }
    }

    /// <summary>
    /// tests create and read action for profiles tables using controller interfaces and services
    /// </summary>
    [Fact]
    public async void ProfileControllerCreateTest()
    {
      DbContextOptions<LTBDBContext> options =
            new DbContextOptionsBuilder<LTBDBContext>()
            .UseInMemoryDatabase("Profiles")
            .Options;
      using (LTBDBContext context = new LTBDBContext(options))
      {
        ProfileService TestService = new ProfileService(context);
        Profile profile = new Profile();
        profile.Username = "James";
        profile.LocationZip = 98146;
        await TestService.CreateProfile(profile);
        var TestProfile = await TestService.GetProfile("James");
        Assert.Equal("James", TestProfile.Username);
      }
    }

    /// <summary>
    /// tests create and read action for favorites tables using controller interfaces and services
    /// </summary>
    [Fact]
    public async void FavoriteControllerCreateTest()
    {
      DbContextOptions<LTBDBContext> options =
            new DbContextOptionsBuilder<LTBDBContext>()
            .UseInMemoryDatabase("Favorites")
            .Options;
      using (LTBDBContext context = new LTBDBContext(options))
      {
        FavoriteService TestService = new FavoriteService(context);
        Favorite favorites = new Favorite();
        favorites.UserID= 1;
        favorites.PetID = 2;
        favorites.Notes = "Did this test pass?";
        await TestService.CreateFavorite(favorites);
        var TestFavorite = await TestService.GetFavorite(1,2);
        Assert.Equal("Did this test pass?", TestFavorite.Notes);
      }
    }

    /// <summary>
    /// tests update and read action for favorites tables using controller interfaces and services
    /// </summary>
    [Fact]
    public async void FavoriteControllerUpdateTest()
    {
      DbContextOptions<LTBDBContext> options =
            new DbContextOptionsBuilder<LTBDBContext>()
            .UseInMemoryDatabase("FavoritesUpdate")
            .Options;
      using (LTBDBContext context = new LTBDBContext(options))
      {
        FavoriteService TestService = new FavoriteService(context);
        Favorite favorites = new Favorite();
        favorites.UserID = 1;
        favorites.PetID = 2;
        favorites.Notes = "Did this test pass?";
        await TestService.CreateFavorite(favorites);

        favorites.Notes = "Let's change the test";

        await TestService.UpdateFavorite(favorites);

        var TestFavorite = await TestService.GetFavorite(favorites.UserID, favorites.PetID);
        Assert.Equal("Let's change the test", TestFavorite.Notes);
      }
    }

    /// <summary>
    /// tests delete action for favorites tables using controller interfaces and services
    /// should return null
    /// </summary>
    [Fact]
    public async void FavoriteControllerDeleteTest()
    {
      DbContextOptions<LTBDBContext> options =
            new DbContextOptionsBuilder<LTBDBContext>()
            .UseInMemoryDatabase("FavoritesDelete")
            .Options;
      using (LTBDBContext context = new LTBDBContext(options))
      {
        FavoriteService TestService = new FavoriteService(context);
        Favorite favorites = new Favorite();
        favorites.UserID = 1;
        favorites.PetID = 2;
        favorites.Notes = "Did this test pass?";
        await TestService.CreateFavorite(favorites);

        await TestService.DeleteFavorite(1,2);
        var TestFavorite = await context.Favorites.FirstOrDefaultAsync(x => x.UserID == 1 && x.PetID == 2);
        Assert.Null(TestFavorite);
      }
    }

    /// <summary>
    /// tests JSON get/set
    /// </summary>
    [Fact]
    public void PetJSONGetSetTest()
    {
      PetJSON Pet = new PetJSON();
      Pet.petfinder = new Petfinder();
      Pet.petfinder.pet = new Pet();
      Pet.petfinder.pet.name = new Name();
      Pet.petfinder.pet.name.data = "This is a note.";

      Assert.Equal("This is a note.", Pet.petfinder.pet.name.data);
    }

    /// <summary>
    /// tests JSON updates
    /// </summary>
    [Fact]
    public void PetJSONGetUpdateTest()
    {
      PetJSON Pet = new PetJSON();
      Pet.petfinder = new Petfinder();
      Pet.petfinder.pet = new Pet();
      Pet.petfinder.pet.name = new Name();
      Pet.petfinder.pet.name.data = "This is a note.";
      Pet.petfinder.pet.name.data = "This is an updated note.";

      Assert.Equal("This is an updated note.", Pet.petfinder.pet.name.data);
    }

    /// <summary>
    /// tests PetReview Get/set
    /// </summary>
    [Fact]
    public void PetReviewGetSetTest()
    {
      PetReview Pet = new PetReview();
      Pet.Intelligent = true;
      Pet.HighEnergy = true;
      Pet.Healthy = true;
      Pet.Playful = true;
      Pet.UserID = 1;
      Pet.PetID = 2;
      Pet.Cheery = true;
      Pet.Drool = true;
      Pet.Affectionate = true;
      Pet.Friendly = true;
      Pet.Impression = "This impression";
      Assert.Equal("This impression", Pet.Impression);
      Assert.True(Pet.Intelligent);
      Assert.True(Pet.Healthy);
      Assert.True(Pet.Playful);
      Assert.True(Pet.Cheery);
      Assert.True(Pet.Drool);
      Assert.True(Pet.HighEnergy);
      Assert.True(Pet.Affectionate);
      Assert.True(Pet.Friendly);
      Assert.Equal(1, Pet.UserID);
      Assert.Equal(2, Pet.PetID);
    }

    /// <summary>
    /// tests PetReview updates
    /// </summary>
    [Fact]
    public void PetReviewGetUpdateTest()
    {
      PetReview Pet = new PetReview();
      Pet.Impression = "This impression";
      Pet.Impression = "This is not";
      Pet.Intelligent = true;
      Pet.HighEnergy = true;
      Pet.Healthy = true;
      Pet.Playful = true;
      Pet.UserID = 1;
      Pet.PetID = 2;
      Pet.Cheery = true;
      Pet.Drool = true;
      Pet.Affectionate = true;
      Pet.Friendly = true;
      Pet.Intelligent = false;
      Pet.HighEnergy = false;
      Pet.Healthy = false;
      Pet.Playful = false;
      Pet.UserID = 2;
      Pet.PetID = 3;
      Pet.Cheery = false;
      Pet.Drool = false;
      Pet.Affectionate = false;
      Pet.Friendly = false;
      Assert.False(Pet.Intelligent);
      Assert.False(Pet.Healthy);
      Assert.False(Pet.Playful);
      Assert.False(Pet.Cheery);
      Assert.False(Pet.Drool);
      Assert.False(Pet.HighEnergy);
      Assert.False(Pet.Affectionate);
      Assert.False(Pet.Friendly);
      Assert.Equal(2, Pet.UserID);
      Assert.Equal(3, Pet.PetID);
      Assert.Equal("This is not", Pet.Impression);
    }


    /// <summary>
    /// tests Registererror
    /// </summary>
    [Fact]
    public void RegisterErrorGetSetTest()
    {
      RegisterError Error = new RegisterError();
      Error.userExist = false;
      Assert.False(Error.userExist);
    }

    /// <summary>
    /// tests register error updates
    /// </summary>
    [Fact]
    public void RegisterErrorGetUpdateTest()
    {
      RegisterError Error = new RegisterError();
      Error.userExist = false;
      Error.userExist = true;
      Assert.True(Error.userExist);
    }

    /// <summary>
    /// tests PetPost get/set
    /// </summary>
    [Fact]
    public void PetPostGetSetTest()
    {
      PetPost Pet = new PetPost();
      Pet.Name = "Fluffy";
      Pet.Mix = "Fluffy";
      Pet.PetID = 1;
      Pet.Phone="Fluffy";
      Pet.Photos = "Fluffy";
      Pet.ShelterID = "Fluffy";
      Assert.Equal("Fluffy", Pet.Mix);
      Assert.Equal("Fluffy", Pet.Phone);
      Assert.Equal("Fluffy", Pet.Photos);
      Assert.Equal("Fluffy", Pet.ShelterID);
      Assert.Equal("Fluffy", Pet.Name);
    }

    /// <summary>
    /// tests PetPost updates
    /// </summary>
    [Fact]
    public void PetPostGetUpdateTest()
    {
      PetPost Pet = new PetPost();
      Pet.Name = "Fluffy";
      Pet.Name = "Daany";
      Assert.Equal("Daany", Pet.Name);
    }
  }
}
