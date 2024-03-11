using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRenting.ViewModels
{
    public class CarGalleryRegViewModel
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

        [Required(ErrorMessage = "Pleas Enter The Location")]
        public string Location { get; set; }
       
        public string Tax_Card_Name { get; set; }
        [Required(ErrorMessage = "Pleas Upload Your Tax Card")]
        [NotMapped]
        public IFormFile Tax_Card_Url { get; set; }

       
        public string Commercial_Registration_Name { get; set; }
        [Required(ErrorMessage = "Pleas Upload Your Commercial Registration")]
        [NotMapped]
        public IFormFile Commercial_Registration_Url { get; set; }


        
        public string ImageName { get; set; }
        [Required(ErrorMessage = "Please Upload Images")]
        [NotMapped]
        public List <IFormFile> ImageUrl { get; set; }
    }
}
