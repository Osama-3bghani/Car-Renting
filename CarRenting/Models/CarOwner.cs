namespace CarRenting.Models
{
    public class CarOwner:User
    {
        public int CarId { get; set; }
        public Car Car { get; set; }

        public virtual ICollection<CarImagesCarOwner> CarImagesCarOwners { get; set; } = new HashSet<CarImagesCarOwner>();
    }
}
