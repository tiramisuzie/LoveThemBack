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
    /// tests create list
    /// </summary>
    [Fact]
    public void CreateToDoList()
    {
      DbContextOptions<Pet> options =
             new DbContextOptionsBuilder<Pet>().UseInMemoryDatabase("CreateToDoItem")
             .Options;


      using (ToDoContext context = new ToDoContext(options))
      {
        var list = new ToDoList();
        list.ListID = 1;
        list.ListName = "Dummy List";

        var controllerCreate = new ToDoListController(context);
        controllerCreate.Create(list);
        var getList = controllerCreate.GetById(1);

        Assert.Equal(list, getList.Value);
      }
    }
  }
}
