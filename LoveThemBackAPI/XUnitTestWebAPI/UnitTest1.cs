using System;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoveThemBackAPI;
using LoveThemBackAPI.Controllers;
using LoveThemBackAPI.Models;
using LoveThemBackAPI.Data;
using LoveThemBackAPI.Models.Services;
using LoveThemBackAPI.Models.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace XUnitTestWebAPI
{
  public class UnitTest1
  {
    /// <summary>
    /// get/set test on Pet profile
    /// </summary>
    [Fact]
    public void GetSetPetName()
    {
      Pet TestPet = new Pet();
      TestPet.Animal = "Dog";
      TestPet.Name = "Dog";
      TestPet.PetID = 1;
      TestPet.Mix = "Dog";
      TestPet.Phone = "Dog";
      TestPet.ShelterID="Dog";
      Assert.Equal("Dog", TestPet.Animal);
      Assert.Equal("Dog", TestPet.Name);
      Assert.Equal("Dog", TestPet.Mix);
      Assert.Equal("Dog", TestPet.Phone);
      Assert.Equal("Dog", TestPet.ShelterID);
    }

    /// <summary>
    /// update test on Pet profiles
    /// </summary>
    [Fact]
    public void UpdatePetName()
    {
      Pet TestPet = new Pet();
      TestPet.Animal = "Dog";
      TestPet.Animal = "Cat";

      TestPet.Name = "Dog";
      TestPet.PetID = 1;
      TestPet.Mix = "Dog";
      TestPet.Phone = "Dog";
      TestPet.ShelterID = "Dog";

      TestPet.Name = "Cat";
      TestPet.PetID = 2;
      TestPet.Mix = "Cat";
      TestPet.Phone = "Cat";
      TestPet.ShelterID = "Cat";

      Assert.Equal("Cat", TestPet.Animal);
      Assert.Equal("Cat", TestPet.Name);
      Assert.Equal(2, TestPet.PetID);
      Assert.Equal("Cat", TestPet.Mix);
      Assert.Equal("Cat", TestPet.Phone);
      Assert.Equal("Cat", TestPet.ShelterID);
    }

    /// <summary>
    /// get/set reviews test
    /// </summary>
    [Fact]
    public void GetSetReview()
    {
      Review Pet = new Review();
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
    /// update reviews test
    /// </summary>
    [Fact]
    public void UpdateReview()
    {
      Review Pet = new Review();
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
    /// tests create and read Pets in database using services
    /// </summary>
    [Fact]
    public void CreatePet()
    {
      DbContextOptions<LoveThemBackAPIDbContext> options =
             new DbContextOptionsBuilder<LoveThemBackAPIDbContext>().UseInMemoryDatabase("CreatePet")
             .Options;


      using (LoveThemBackAPIDbContext context = new LoveThemBackAPIDbContext(options))
      {
        var TestPet = new Pet();
        TestPet.PetID = 1;
        TestPet.Name = "Snooky";

        var ServicesCreate = new PetsService(context);
        ServicesCreate.AddPet(TestPet);
        var getPet = ServicesCreate.GetById(1);

        Assert.Equal("Snooky", getPet.Value.Name);
      }
    }

    /// <summary>
    /// tests update Pet operation
    /// </summary>
    [Fact]
    public void UpdatePet()
    {
      DbContextOptions<LoveThemBackAPIDbContext> options =
             new DbContextOptionsBuilder<LoveThemBackAPIDbContext>().UseInMemoryDatabase("UpdatePet")
             .Options;


      using (LoveThemBackAPIDbContext context = new LoveThemBackAPIDbContext(options))
      {
        Pet TestPet = new Pet();
        TestPet.PetID = 1;
        TestPet.Name = "Snooky";

        var ServicesCreate = new PetsService(context);
        ServicesCreate.AddPet(TestPet);

        Pet UpdatePet = new Pet();
        UpdatePet.PetID = 1;
        UpdatePet.Name = "Coolio";

        ServicesCreate.UpdatePet(1, UpdatePet);
        var getPet = ServicesCreate.GetById(1);

        Assert.Equal("Coolio", getPet.Value.Name);
      }
    }

    /// <summary>
    /// tests delete Pet operation
    /// </summary>
    [Fact]
    public void DeletePet()
    {
      DbContextOptions<LoveThemBackAPIDbContext> options =
             new DbContextOptionsBuilder<LoveThemBackAPIDbContext>().UseInMemoryDatabase("DeletePet")
             .Options;


      using (LoveThemBackAPIDbContext context = new LoveThemBackAPIDbContext(options))
      {
        Pet TestPet = new Pet();
        TestPet.PetID = 1;
        TestPet.Name = "Snooky";

        var ServicesCreate = new PetsService(context);
        ServicesCreate.AddPet(TestPet);
        ServicesCreate.DeletePet(1, 8675309);
        var getPet = context.Pets.FirstOrDefault(x => x.PetID == 1);

        Assert.Null(getPet);
      }
    }

    /// <summary>
    /// tests create and read Review usings services
    /// </summary>
    [Fact]
    public async void CreateReview()
    {
      DbContextOptions<LoveThemBackAPIDbContext> options =
             new DbContextOptionsBuilder<LoveThemBackAPIDbContext>().UseInMemoryDatabase("CreateReview")
             .Options;


      using (LoveThemBackAPIDbContext context = new LoveThemBackAPIDbContext(options))
      {
        var Review = new Review();
        Review.PetID = 1;
        Review.UserID = 2;
        Review.Impression = "Snooky";

        var ServicesCreate = new ReviewsService(context);
        await ServicesCreate.AddReview(Review);
        //READ//
        var getReview = ServicesCreate.GetById(1);

        string impression;
        foreach (var item in getReview.Value)
        {
          impression = item.Impression;
          Assert.Equal("Snooky", impression);
        }
      }
    }

    /// <summary>
    /// tests getting all reivews
    /// </summary>
    [Fact]
    public async void GetAllReview()
    {
      DbContextOptions<LoveThemBackAPIDbContext> options =
             new DbContextOptionsBuilder<LoveThemBackAPIDbContext>().UseInMemoryDatabase("AllReview")
             .Options;


      using (LoveThemBackAPIDbContext context = new LoveThemBackAPIDbContext(options))
      {
        var Review = new Review();
        Review.PetID = 1;
        Review.UserID = 2;
        Review.Impression = "Snooky";

        var Review2 = new Review();
        Review2.PetID = 2;
        Review2.UserID = 2;
        Review2.Impression = "Snooky";

        var ServicesCreate = new ReviewsService(context);
        await ServicesCreate.AddReview(Review);
        await ServicesCreate.AddReview(Review2);

        //READ//
        var getReview = ServicesCreate.GetAll();
        Assert.Equal(2, getReview.Value.Count());
      }
    }

    /// <summary>
    /// tests update review operation
    /// </summary>
    [Fact]
    public async void UpdateReviewOp()
    {
      DbContextOptions<LoveThemBackAPIDbContext> options =
             new DbContextOptionsBuilder<LoveThemBackAPIDbContext>().UseInMemoryDatabase("UpdateReview")
             .Options;


      using (LoveThemBackAPIDbContext context = new LoveThemBackAPIDbContext(options))
      {
        Review Review = new Review();
        Review.PetID = 1;
        Review.UserID = 2;
        Review.Impression = "Snooky";

        var ServicesCreate = new ReviewsService(context);
        await ServicesCreate.AddReview(Review);

        Review newReview = new Review();

        newReview.PetID = 1;
        newReview.UserID = 2;
        newReview.Impression = "Updates";

        ServicesCreate.UpdateReview(2, 1, newReview);
        var getReview = ServicesCreate.GetById(1);
        string impression;
        foreach (var item in getReview.Value)
        {
          impression = item.Impression;
          Assert.Equal("Updates", impression);
        }
      }
    }
  

    /// <summary>
    /// tests delete Review
    /// </summary>
    [Fact]
    public async void DeleteReview()
    {
      DbContextOptions<LoveThemBackAPIDbContext> options =
             new DbContextOptionsBuilder<LoveThemBackAPIDbContext>().UseInMemoryDatabase("DeletePet")
             .Options;


      using (LoveThemBackAPIDbContext context = new LoveThemBackAPIDbContext(options))
      {
        Review Review = new Review();
        Review.PetID = 1;
        Review.UserID = 2;
        Review.Impression = "Snooky";

        var ServicesCreate = new ReviewsService(context);
        await ServicesCreate.AddReview(Review);

        ServicesCreate.DeleteReview(2, 1);
        var getReview = context.Reviews.FirstOrDefault(x => x.PetID == 1 && x.UserID == 2);

        Assert.Null(getReview);
      }
    }
  }
}
