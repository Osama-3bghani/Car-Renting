using CarRenting.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRenting.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login(string email,string password)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var user = context.Customers.FirstOrDefault(x=>x.Email==email && x.Password==password);
            if (user != null) 
            {
                HttpContext.Session.SetInt32("SessionUserId",user.Id);
                HttpContext.Session.SetString("SessionUserEmail",user.Email);
                return View("Profile", user);
            }

            var carOnwerCar = context.CarImagesCarOwners.Include(x => x.CarOwner).Include(x=>x.Car).Include(x=>x.CarImages);
            var carOwner = carOnwerCar.FirstOrDefault(x=>x.CarOwner.Email==email && x.CarOwner.Password==password);
            if (carOwner != null) 
            {
                HttpContext.Session.SetString("SessionUserEmail", carOwner.CarOwner.Email);
                HttpContext.Session.SetInt32("SessionUserId", carOwner.CarOwner.Id);
                return View("CarOwnerProfile", carOwner);
            }
            var carGalleryImages = context.CarGalleryImages.Include(x => x.CarGallery);
            var carGallery = carGalleryImages.FirstOrDefault(x => x.CarGallery.Email == email && x.CarGallery.Password == password);
            if (carGallery != null)
            {
                HttpContext.Session.SetString("SessionUserEmail", carGallery.CarGallery.Email);
                HttpContext.Session.SetInt32("SessionUserId", carGallery.CarGallery.Id);
                return View("CarGalleryProfile", carGallery);
            }

            return Json("Wrong Email Or Password");
        }
    }
}
