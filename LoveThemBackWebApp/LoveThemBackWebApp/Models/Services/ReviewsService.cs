using LoveThemBackWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using LoveThemBackWebApp.Models.Interfaces;

namespace LoveThemBackWebApp.Models.Services
{
  public class ReviewsService : IReviews
  {
    private LTBDBContext _context;

    public ReviewsService(LTBDBContext context)
    {
      _context = context;
    }
    /// <summary>
    /// method called to create new reviews
    /// </summary>
    /// <param name="review">review from user</param>
    /// <returns></returns>
    public async Task<IActionResult> PostReview(Reviews review)
    {
      string output = await Task.Run(() => JsonConvert.SerializeObject(review));
      var httpContent = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
      using (HttpClient httpClient = new HttpClient())
      {
        var httpResponse = await httpClient.PostAsync("https://lovethembackapi2.azurewebsites.net/api/Reviews", httpContent);
        if (httpResponse.Content != null)
        {
          var responseContent = await httpResponse.Content.ReadAsStringAsync();
        }
      }
      return null;
    }
    /// <summary>
    /// method called to edit existing review
    /// </summary>
    /// <param name="review">review with edit contents</param>
    /// <returns></returns>
    public async Task<IActionResult> PutReview(Reviews review)
    {
      string output = await Task.Run(() => JsonConvert.SerializeObject(review));
      var httpContent = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
      using (HttpClient httpClient = new HttpClient())
      {
        string url = "https://lovethembackapi2.azurewebsites.net/api/Reviews/" + review.UserID + "/" + review.PetID;
        var httpResponse = await httpClient.PutAsync(url, httpContent);
        if (httpResponse.Content != null)
        {
          var responseContent = await httpResponse.Content.ReadAsStringAsync();
        }
      }
      return null;
    }
  }
}
