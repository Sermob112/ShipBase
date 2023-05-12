using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipBase.DAL;
using ShipBase.Domain.SectionOne.ViewModels.PurchasingData;

namespace ShipBase.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var query = from b in _context.PurchasingDatas
                        join a in _context.Customers on b.Id equals a.purchasing_id
                        select new AllDataViewModel
                        {
                            Name_of_organization = a.Name_of_organization,
                            Purchase_object = b.Purchase_object
                        };

            var result = query.ToList();

            return View(result);
        }
    }
}
