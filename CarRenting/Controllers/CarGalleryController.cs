using CarRenting.Models.Context;
using CarRenting.Models;
using CarRenting.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace CarRenting.Controllers
{
    public class CarGalleryController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public CarGalleryController(IWebHostEnvironment hostEnvironment)
        {

            _hostEnvironment = hostEnvironment;
        }
        public IActionResult CreateCarGallery()
        {
            return View();
        }
        public IActionResult Add(CarGalleryRegViewModel carGallery)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateCarGallery", carGallery);
            }
            ApplicationDbContext context = new ApplicationDbContext();
           

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(carGallery.ProfileImageUrl.FileName);
            string extension = Path.GetExtension(carGallery.ProfileImageUrl.FileName);
            carGallery.ProfileImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Images/", fileName);
            var fileStream = new FileStream(path, FileMode.Create);
            carGallery.ProfileImageUrl.CopyTo(fileStream);

            string wwwRootPath1 = _hostEnvironment.WebRootPath;
            string fileName1 = Path.GetFileNameWithoutExtension(carGallery.Tax_Card_Url.FileName);
            string extension1 = Path.GetExtension(carGallery.Tax_Card_Url.FileName);
            carGallery.Tax_Card_Name = fileName1 = fileName1 + DateTime.Now.ToString("yymmssfff") + extension1;
            string path1 = Path.Combine(wwwRootPath1 + "/Images/", fileName1);
            var fileStream1 = new FileStream(path1, FileMode.Create);
            carGallery.Tax_Card_Url.CopyTo(fileStream1);

            string wwwRootPath2 = _hostEnvironment.WebRootPath;
            string fileName2 = Path.GetFileNameWithoutExtension(carGallery.Commercial_Registration_Url.FileName);
            string extension2 = Path.GetExtension(carGallery.Commercial_Registration_Url.FileName);
            carGallery.Commercial_Registration_Name = fileName2 = fileName2 + DateTime.Now.ToString("yymmssfff") + extension2;
            string path2 = Path.Combine(wwwRootPath2 + "/Images/", fileName2);
            var fileStream2 = new FileStream(path2, FileMode.Create);
            carGallery.Commercial_Registration_Url.CopyTo(fileStream2);


            var gallery = new CarGallery()
            {
                FirstName = carGallery.FirstName,
                LastName = carGallery.LastName,
                Email = carGallery.Email,
                BirthDate = carGallery.BirthDate,
                Password = carGallery.Password,
                Phone = carGallery.Phone,
                UserName = carGallery.UserName,
                NationalId = carGallery.NationalId,
                ProfileImageName = carGallery.ProfileImageName,
                Location = carGallery.Location,
                Tax_Card_Name = carGallery.Tax_Card_Name,
                Commercial_Registration_Name = carGallery.Commercial_Registration_Name
            };
            context.CarGallery.Add(gallery);
            context.SaveChanges();

            string rootPath = _hostEnvironment.WebRootPath;
            foreach (var item in carGallery.ImageUrl)
            {
                string imageName = Path.GetFileNameWithoutExtension(item.FileName);
                string extensions = Path.GetExtension(item.FileName);
                var name = imageName = imageName + DateTime.Now.ToString("yymmssfff") + extensions;
                string paths = Path.Combine(rootPath + "/Images/", imageName);
                var fileStreams = new FileStream(paths, FileMode.Create);
                item.CopyTo(fileStreams);

                var carGalleryImages = new CarGalleryImages() 
                {
                   ImageName = name,
                   CarGalleryId=gallery.Id,
                };
                context.CarGalleryImages.Add(carGalleryImages);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
