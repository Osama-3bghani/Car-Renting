using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRenting.Models
{
    public class CarGallery:User
    {
        public CarGallery()
        {
            Cars=new HashSet<Car>();   
            CarGalleryImages= new HashSet<CarGalleryImages>();
        }
        [Required (ErrorMessage ="Pleas Enter The Location")]
        public string Location { get; set; }
       
        public string Tax_Card_Name { get; set; }
        [Required(ErrorMessage = "Pleas Upload Your Tax Card")]
        [NotMapped]
        public IFormFile Tax_Card_Url { get; set; }
        
        public string Commercial_Registration_Name { get; set; }
        [Required(ErrorMessage = "Pleas Upload Your Commercial Registration")]
        [NotMapped]
        public IFormFile Commercial_Registration_Url { get; set; }

        public ICollection<Car> Cars { get; set; }
        public ICollection<CarGalleryImages> CarGalleryImages{ get; set; }
    }
}
