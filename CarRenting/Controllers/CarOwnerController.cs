using CarRenting.Models;
using CarRenting.Models.Context;
using CarRenting.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace CarRenting.Controllers
{
    public class CarOwnerController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public CarOwnerController(IWebHostEnvironment hostEnvironment)
        {

            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Add(CarOwnerRegViewModel carOwner)
        {
            if (!ModelState.IsValid) 
            {
                return View("Create",carOwner);
            }
            ApplicationDbContext context = new ApplicationDbContext();
            var car = new Car()
            {
                CarName = carOwner.CarName,
                BrandName = carOwner.BrandName,
                Price = carOwner.Price,
                ProvidedBy = carOwner.ProvidedBy,
                Availability = carOwner.Availability,
                CarStatus = carOwner.CarStatus,
                Description = carOwner.Description,
                ModelYear = carOwner.ModelYear,
                CarMoved = carOwner.CarMoved,
                Offer = carOwner.Offer,
                CarType = carOwner.CarType,
            };
            context.Cars.Add(car);
            context.SaveChanges();

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(carOwner.ProfileImageUrl.FileName);
            string extension = Path.GetExtension(carOwner.ProfileImageUrl.FileName);
            carOwner.ProfileImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Images/", fileName);
            var fileStream = new FileStream(path, FileMode.Create);
            carOwner.ProfileImageUrl.CopyTo(fileStream);

            var owner = new CarOwner
            {
                FirstName = carOwner.FirstName,
                LastName = carOwner.LastName,
                Email = carOwner.Email,
                BirthDate = carOwner.BirthDate,
                Password = carOwner.Password,
                Phone = carOwner.Phone,
                UserName = carOwner.UserName,
                NationalId = carOwner.NationalId,
                ProfileImageName = carOwner.ProfileImageName,
                CarId = car.Id,
            };

            context.CarOwners.Add(owner);
            context.SaveChanges();

            string rootPath = _hostEnvironment.WebRootPath;
            foreach (var item in carOwner.ImageUrl)
            {
                string imageName = Path.GetFileNameWithoutExtension(item.FileName);
                string extensions = Path.GetExtension(item.FileName);
                var name = imageName = imageName + DateTime.Now.ToString("yymmssfff") + extensions;
                string paths = Path.Combine(rootPath + "/Images/", imageName);
                var fileStreams = new FileStream(paths, FileMode.Create);
                item.CopyTo(fileStreams);

                var imagesOfCars = new CarImages
                {
                    ImageName = name,
                    CarId = car.Id,
                };
                context.CarImages.Add(imagesOfCars); 
                context.SaveChanges();

                CarImagesCarOwner carImagesCarOwner = new CarImagesCarOwner
                {
                    CarId = car.Id,
                    CarImagesId = imagesOfCars.Id,
                    CarOwnerId = owner.Id
                };
                context.CarImagesCarOwners.Add(carImagesCarOwner);
                context.SaveChanges();
            }

            return RedirectToAction("Index","Home");
        }
   
    }
}
