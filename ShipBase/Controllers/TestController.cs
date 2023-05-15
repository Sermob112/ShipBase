using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipBase.DAL;
using ShipBase.Domain.SectionOne.Extensions;
using ShipBase.Domain.SectionOne.ViewModels.PurchasingData;
using ShipBase.Service.SectionOne.Implementations;
using ShipBase.Service.SectionOne.Interfaces;
using System.Linq;

namespace ShipBase.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPurchService _purchaservice;
        public TestController(ApplicationDbContext context, IPurchService purchaservice)
        {
            _purchaservice = purchaservice;
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
        public IActionResult ExemelReader()
        {
            /*  var query = from b in _context.PurchasingDatas
                          join a in _context.Customers on b.Id equals a.purchasing_id
                          select new AllDataViewModel
                          {
                              Name_of_organization = a.Name_of_organization,
                              Purchase_object = b.Purchase_object
                          };

              var result = query.ToList();*/

            return View(/*result*/);
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file)
        {
      
                var response = await _purchaservice.Create(file);
                if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
                {
                return RedirectToAction("GetPurchs");
            }
            else { return Json(new { description = response.Description }); }

  

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetPurchs()
        {
            var response = _purchaservice.GetPurchasingDatas();
            if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
    }
}
