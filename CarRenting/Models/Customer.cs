namespace CarRenting.Models
{
    public class Customer:User
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
        }
        public ICollection<Booking> Bookings { get; set; }
    }
}
