using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipBase.DAL;

namespace ShipBase.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.PurchasingDatas
                .Include(c => c.Customers)
                .ToListAsync();

            return View(customers);
        }
    }
}
