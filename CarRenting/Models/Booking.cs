using CarRenting.Models.Enums;

namespace CarRenting.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Price { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public Customer Customer { get; set; }
        public int SenderId { get; set; }

        public Car Car { get; set; }
        public int RecieverId { get; set; }

    }
   
}
