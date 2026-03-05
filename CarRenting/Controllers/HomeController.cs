using CarRenting.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarRenting.Controllers
{
    public class HomeController : Controller
    {
        // FIX 1: Inject DbContext instead of newing it up manually
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // FIX 2: Removed redundant .Distinct() before .ToList() —
            // EF Core's Distinct() on an entity with no equality comparer
            // does nothing useful here; DistinctBy(x => x.CarId) already
            // handles deduplication correctly after materializing.
            var cars = _context.CarImages
                .Include(x => x.Car)
                .ToList()
                .DistinctBy(x => x.CarId);

            return View("Index", cars);
        }
    }
}