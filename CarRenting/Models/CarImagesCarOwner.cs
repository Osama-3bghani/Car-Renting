using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRenting.Models
{
    public class CarImagesCarOwner
    {

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int CarImagesId { get; set; }
        public CarImages CarImages { get; set; }

        public int CarOwnerId { get; set; }
        public CarOwner CarOwner { get; set; }
    }
}
