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
      Assert.Equal("Dog", TestPet.Animal);
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
      Assert.Equal("Cat", TestPet.Animal);
    }

    /// <summary>
    /// get/set reviews test
    /// </summary>
    [Fact]
    public void GetSetReview()
    {
      Review TestReview = new Review();
      TestReview.Impression = "Yay!";
      Assert.Equal("Yay!", TestReview.Impression);
    }

    /// <summary>
    /// update reviews test
    /// </summary>
    [Fact]
    public void UpdateReview()
    {
      Review TestReview = new Review();
      TestReview.Impression = "Yay!";
      TestReview.Impression = "Boo!";
      Assert.Equal("Boo!", TestReview.Impression);
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
