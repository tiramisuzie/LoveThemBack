using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveThemBackWebApp.Models.Interfaces
{
    public interface IReviews
    {
        Task<IActionResult> PostReview(Reviews review);
    }
}
