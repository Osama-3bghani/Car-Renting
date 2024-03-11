using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarRenting.Models.Enums;

namespace CarRenting.ViewModels
{
    public class CarOwnerRegViewModel
    {
        [Required(ErrorMessage = "Please Enter FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter PhoneNumber")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Pleas Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Pleas Enter NationalId")]
        public long NationalId { get; set; }
        public string ProfileImageName { get; set; }

        [NotMapped]
        public IFormFile ProfileImageUrl { get; set; }

        [Required(ErrorMessage = "Pleas Enter BirthDate")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Please Enter CarName")]
        public string CarName { get; set; }
        [Required(ErrorMessage = "Pleas Enter BrandName")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Please Enter Price")]
        public double Price { get; set; }

        public string Description { get; set; }

        public double CarMoved { get; set; }
        public double Offer { get; set; }
        [Required(ErrorMessage = "Please Select")]
        public CarType CarType { get; set; }
        [Required(ErrorMessage = "Please Select")]
        public ProvidedBy ProvidedBy { get; set; }
        [Required(ErrorMessage = "Please Select")]
        public CarStatus CarStatus { get; set; }
        [Required(ErrorMessage = "Please Select")]
        public Availability Availability { get; set; }
        [Required(ErrorMessage = "Please Enter ModelYear")]
        public int ModelYear { get; set; }

        
        public string ImageName { get; set; }
        [Required(ErrorMessage = "Please Upload Images")]
        [NotMapped]
        public List <IFormFile> ImageUrl { get; set; }
    }
}
