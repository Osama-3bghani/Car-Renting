using CarRenting.Models;
using CarRenting.Models.Context;
using CarRenting.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CarRenting.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public CustomerController(IWebHostEnvironment hostEnvironment)
        {

            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Register() 
        {
            return View();
        }
        public IActionResult AddCustomer(Customer customer)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(customer.ProfileImageUrl.FileName);
            string extension = Path.GetExtension(customer.ProfileImageUrl.FileName);
            customer.ProfileImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Images/", fileName);
            var fileStream = new FileStream(path, FileMode.Create);
            customer.ProfileImageUrl.CopyTo(fileStream);
            context.Add(customer);
            context.SaveChanges();
            return RedirectToAction("Home","Index");
        }

        public IActionResult CarForRent()
        {
            
            ApplicationDbContext context = new ApplicationDbContext();
            var forRent = context.CarImages.Include(x => x.Car)
                .Where(x => x.Car.CarType == CarType.Rent).ToList().DistinctBy(x=>x.CarId);            
            return View("CarRent", forRent);
        }
        public IActionResult CarForBuying()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var forBuying = context.CarImages.Include(x => x.Car)
                .Where(z => z.Car.CarType == CarType.Sell)
                .ToList().DistinctBy(x => x.CarId);
            return View("CarBuy", forBuying);
        }
    }
}
