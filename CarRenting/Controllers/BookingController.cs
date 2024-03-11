using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Reservation(int id)
        {
            return View();
        }
    }
}
