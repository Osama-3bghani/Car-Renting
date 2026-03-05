using CarRenting.Models;
using CarRenting.Models.Context;
using CarRenting.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRenting.Controllers
{
    public class CustomerController : Controller
    {
        // FIX 10: Inject both dependencies — DbContext no longer newed up manually
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CustomerController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult AddCustomer(Customer customer)
        {
            // FIX 11: Added ModelState validation — was missing entirely
            if (!ModelState.IsValid)
                return View("Register", customer);

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(customer.ProfileImageUrl.FileName);
            string extension = Path.GetExtension(customer.ProfileImageUrl.FileName);

            // FIX 12: Added Guid to filename to prevent collisions when two uploads happen
            // in the same millisecond (the old timestamp alone was not unique enough)
            customer.ProfileImageName = fileName =
                fileName + DateTime.Now.ToString("yymmssfff") + Guid.NewGuid().ToString("N") + extension;

            string path = Path.Combine(wwwRootPath, "Images", fileName);

            // FIX 13: FileStream was never disposed — wrapped in using to release the file handle
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                customer.ProfileImageUrl.CopyTo(fileStream);
            }

            _context.Add(customer);
            _context.SaveChanges();

            // FIX 14: RedirectToAction arguments were swapped — (action, controller), not (controller, action)
            // Original: RedirectToAction("Home", "Index")  →  tried to call Index controller, Home action
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CarForRent()
        {
            var forRent = _context.CarImages
                .Include(x => x.Car)
                .Where(x => x.Car.CarType == CarType.Rent)
                .ToList()
                .DistinctBy(x => x.CarId);

            return View("CarRent", forRent);
        }

        public IActionResult CarForBuying()
        {
            var forBuying = _context.CarImages
                .Include(x => x.Car)
                .Where(z => z.Car.CarType == CarType.Sell)
                .ToList()
                .DistinctBy(x => x.CarId);

            return View("CarBuy", forBuying);
        }
    }
}
