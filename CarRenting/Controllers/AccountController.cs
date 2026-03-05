using CarRenting.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRenting.Controllers
{
    public class AccountController : Controller
    {
        // FIX 5: Inject DbContext instead of instantiating with new
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // FIX 6: Changed from GET to POST — credentials must never travel in the URL/query string
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Customers.FirstOrDefault(x => x.Email == email);
            if (user != null && user.Password == password)
            {
                HttpContext.Session.SetInt32("SessionUserId", user.Id);
                HttpContext.Session.SetString("SessionUserEmail", user.Email);
                return View("Profile", user);
            }

            // FIX 7: Load CarOwner via CarOwners table directly — previous query loaded through
            // the CarImagesCarOwner junction table which fails if the owner has no images yet.
            var carOwner = _context.CarOwners
                .Include(x => x.Car)
                .FirstOrDefault(x => x.Email == email);
            if (carOwner != null && carOwner.Password == password)
            {
                HttpContext.Session.SetString("SessionUserEmail", carOwner.Email);
                HttpContext.Session.SetInt32("SessionUserId", carOwner.Id);

                // FIX 8: Pass CarOwner directly — view needs the owner, not a junction row
                return View("CarOwnerProfile", carOwner);
            }

            // FIX 9: Load CarGallery directly instead of through CarGalleryImages junction —
            // same problem as CarOwner: login fails if no images have been uploaded yet.
            var carGallery = _context.CarGallery
                .FirstOrDefault(x => x.Email == email);
            if (carGallery != null && carGallery.Password == password)
            {
                HttpContext.Session.SetString("SessionUserEmail", carGallery.Email);
                HttpContext.Session.SetInt32("SessionUserId", carGallery.Id);
                return View("CarGalleryProfile", carGallery);
            }

            return Json("Wrong Email Or Password");
        }
    }
}
