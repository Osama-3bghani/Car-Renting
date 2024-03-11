using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRenting.Models
{
    public class CarImages
    {
        public int Id { get; set; }
       
        public string ImageName { get; set; }
        [Required(ErrorMessage = "Please Upload Images")]
        [NotMapped]
        public IFormFile ImageUrl { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public virtual ICollection<CarImagesCarOwner> CarImagesCarOwners { get; set; } = new HashSet<CarImagesCarOwner>();

    }
}
