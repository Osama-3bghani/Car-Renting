using CarRenting.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarRenting.Models
{
    public class Car
    {
        public Car()
        {
            CarOwners = new HashSet<CarOwner>();
            Bookings = new HashSet<Booking>();
            CarImages = new HashSet<CarImages>();
            CarImagesCarOwners = new HashSet<CarImagesCarOwner>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter CarName")]
        public string CarName { get; set; }
        [Required (ErrorMessage = "Pleas Enter BrandName")]
        public string BrandName { get; set; }
        [Required (ErrorMessage ="Please Enter Price")]
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
        [Required (ErrorMessage ="Please Select")]
        public Availability Availability { get; set; }
        [Required(ErrorMessage = "Please Enter Model Year")]
        public int ModelYear { get; set; }
        
        public int? CarGalleryId { get; set; }
        public CarGallery CarGallery { get; set; }

        public ICollection<CarOwner> CarOwners { get; set; }
        public ICollection<Booking> Bookings { get; set; }

        public ICollection<CarImages> CarImages { get; set; }

        public virtual ICollection<CarImagesCarOwner> CarImagesCarOwners { get; set; }
    }
}
