using CarRenting.Models.Context;
using CarRenting.Models;
using CarRenting.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class CarGalleryController : Controller
    {
        // FIX 22: Inject DbContext — was newed up manually in every action
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CarGalleryController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult CreateCarGallery()
        {
            return View();
        }

        public IActionResult Add(CarGalleryRegViewModel carGallery)
        {
            if (!ModelState.IsValid)
                return View("CreateCarGallery", carGallery);

            string wwwRootPath = _hostEnvironment.WebRootPath;

            // FIX 23: All three FileStream objects were never disposed — wrapped each in using.
            // Also added Guid suffix to all filenames to prevent collisions.

            string profileFileName = Path.GetFileNameWithoutExtension(carGallery.ProfileImageUrl.FileName)
                + DateTime.Now.ToString("yymmssfff") + Guid.NewGuid().ToString("N")
                + Path.GetExtension(carGallery.ProfileImageUrl.FileName);
            using (var fs = new FileStream(Path.Combine(wwwRootPath, "Images", profileFileName), FileMode.Create))
                carGallery.ProfileImageUrl.CopyTo(fs);
            carGallery.ProfileImageName = profileFileName;

            string taxFileName = Path.GetFileNameWithoutExtension(carGallery.Tax_Card_Url.FileName)
                + DateTime.Now.ToString("yymmssfff") + Guid.NewGuid().ToString("N")
                + Path.GetExtension(carGallery.Tax_Card_Url.FileName);
            using (var fs = new FileStream(Path.Combine(wwwRootPath, "Images", taxFileName), FileMode.Create))
                carGallery.Tax_Card_Url.CopyTo(fs);
            carGallery.Tax_Card_Name = taxFileName;

            string regFileName = Path.GetFileNameWithoutExtension(carGallery.Commercial_Registration_Url.FileName)
                + DateTime.Now.ToString("yymmssfff") + Guid.NewGuid().ToString("N")
                + Path.GetExtension(carGallery.Commercial_Registration_Url.FileName);
            using (var fs = new FileStream(Path.Combine(wwwRootPath, "Images", regFileName), FileMode.Create))
                carGallery.Commercial_Registration_Url.CopyTo(fs);
            carGallery.Commercial_Registration_Name = regFileName;

            var gallery = new CarGallery
            {
                FirstName                   = carGallery.FirstName,
                LastName                    = carGallery.LastName,
                Email                       = carGallery.Email,
                BirthDate                   = carGallery.BirthDate,
                Password                    = carGallery.Password,
                Phone                       = carGallery.Phone,
                UserName                    = carGallery.UserName,
                NationalId                  = carGallery.NationalId,
                ProfileImageName            = carGallery.ProfileImageName,
                Location                    = carGallery.Location,
                Tax_Card_Name               = carGallery.Tax_Card_Name,
                Commercial_Registration_Name = carGallery.Commercial_Registration_Name
            };
            _context.CarGallery.Add(gallery);
            _context.SaveChanges();

            // FIX 24: SaveChanges() was called once per loop iteration — batched into one call
            foreach (var item in carGallery.ImageUrl)
            {
                string imageName = Path.GetFileNameWithoutExtension(item.FileName)
                    + DateTime.Now.ToString("yymmssfff") + Guid.NewGuid().ToString("N")
                    + Path.GetExtension(item.FileName);

                // FIX 25: FileStream inside loop was never disposed
                using (var fs = new FileStream(Path.Combine(wwwRootPath, "Images", imageName), FileMode.Create))
                    item.CopyTo(fs);

                _context.CarGalleryImages.Add(new CarGalleryImages
                {
                    ImageName    = imageName,
                    CarGalleryId = gallery.Id,
                });
            }

            // Single save for all gallery images
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
