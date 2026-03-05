using CarRenting.Models;
using CarRenting.Models.Context;
using CarRenting.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRenting.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Reservation(int id)
        {
            var car = _context.Cars.FirstOrDefault(x => x.Id == id);
            if (car == null)
                return NotFound();

            return View(car);
        }
        // Add this action to BookingController
        [HttpPost]
        public IActionResult Confirm(int carId, DateTime from, DateTime to)
        {
            var userId = HttpContext.Session.GetInt32("SessionUserId");
            if (userId == null)
                return RedirectToAction("Index", "Home");

            var booking = new Booking
            {
                SenderId      = userId.Value,
                RecieverId    = carId,
                From          = from,
                To            = to,
                RequestStatus = RequestStatus.Pending
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
