using CarRenting.Models;
using CarRenting.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarRenting.Controllers
{
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var cars = context.CarImages.Include(x => x.Car).Distinct().ToList().DistinctBy(x=>x.CarId);
            
            return View("Index",cars);
        }


    }
}