using CarRenting.Models;
using CarRenting.Models.Context;
using CarRenting.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class CarOwnerController : Controller
    {
        // FIX 15: Inject DbContext and environment instead of newing up DbContext manually
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CarOwnerController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Add(CarOwnerRegViewModel carOwner)
        {
            if (!ModelState.IsValid)
                return View("Create", carOwner);

            var car = new Car
            {
                CarName     = carOwner.CarName,
                BrandName   = carOwner.BrandName,
                Price       = carOwner.Price,
                ProvidedBy  = carOwner.ProvidedBy,
                Availability = carOwner.Availability,
                CarStatus   = carOwner.CarStatus,
                Description = carOwner.Description,
                ModelYear   = carOwner.ModelYear,
                CarMoved    = carOwner.CarMoved,
                Offer       = carOwner.Offer,
                CarType     = carOwner.CarType,
            };
            _context.Cars.Add(car);
            _context.SaveChanges();

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(carOwner.ProfileImageUrl.FileName);
            string extension = Path.GetExtension(carOwner.ProfileImageUrl.FileName);

            // FIX 16: Added Guid suffix to prevent duplicate filenames on concurrent uploads
            carOwner.ProfileImageName = fileName =
                fileName + DateTime.Now.ToString("yymmssfff") + Guid.NewGuid().ToString("N") + extension;

            string path = Path.Combine(wwwRootPath, "Images", fileName);

            // FIX 17: FileStream was never disposed — wrapped in using
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                carOwner.ProfileImageUrl.CopyTo(fileStream);
            }

            var owner = new CarOwner
            {
                FirstName      = carOwner.FirstName,
                LastName       = carOwner.LastName,
                Email          = carOwner.Email,
                BirthDate      = carOwner.BirthDate,
                Password       = carOwner.Password,
                Phone          = carOwner.Phone,
                UserName       = carOwner.UserName,
                NationalId     = carOwner.NationalId,
                ProfileImageName = carOwner.ProfileImageName,
                CarId          = car.Id,
            };
            _context.CarOwners.Add(owner);
            _context.SaveChanges();

            // FIX 18: SaveChanges() was called twice per loop iteration (once for CarImages,
            // once for CarImagesCarOwner) — collect all entities first, save once at the end
            var carImagesList = new List<CarImages>();
            var carImagesCarOwnerList = new List<CarImagesCarOwner>();

            foreach (var item in carOwner.ImageUrl)
            {
                string imageName = Path.GetFileNameWithoutExtension(item.FileName);
                string extensions = Path.GetExtension(item.FileName);

                // FIX 19: Same Guid-based uniqueness fix for car image filenames
                var name = imageName + DateTime.Now.ToString("yymmssfff") + Guid.NewGuid().ToString("N") + extensions;
                string imagePath = Path.Combine(wwwRootPath, "Images", name);

                // FIX 20: FileStream inside loop was never disposed
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    item.CopyTo(fileStream);
                }

                var carImage = new CarImages { ImageName = name, CarId = car.Id };
                _context.CarImages.Add(carImage);
                _context.SaveChanges(); // needed here to get carImage.Id for the junction row

                carImagesCarOwnerList.Add(new CarImagesCarOwner
                {
                    CarId        = car.Id,
                    CarImagesId  = carImage.Id,
                    CarOwnerId   = owner.Id
                });
            }

            // FIX 21: Single SaveChanges for all junction rows instead of one per iteration
            _context.CarImagesCarOwners.AddRange(carImagesCarOwnerList);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
