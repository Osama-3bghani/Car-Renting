using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRenting.Models
{
    public class CarGalleryImages
    {
        
        public int Id { get; set; }
        public string ImageName { get; set; }
        [Required (ErrorMessage ="Please Upload Images")]
        [NotMapped]
        public IFormFile ImageUrl { get; set; }

        public int CarGalleryId { get; set; }
        public CarGallery CarGallery { get; set; }
      
    }
}
