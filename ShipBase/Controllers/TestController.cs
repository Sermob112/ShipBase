using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipBase.DAL;
using ShipBase.Domain.SectionOne.Extensions;
using ShipBase.Domain.SectionOne.ViewModels.Purch;
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
        [HttpGet]
        public IActionResult CreateFromFile() => PartialView();
        [HttpPost]
        public async Task<IActionResult> CreateFromFile(IFormFile file)
        {
      
                var response = await _purchaservice.CreateFromFile(file);
                if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
                {
                return RedirectToAction("GetPurchs");
                    }
            else { 
                return Json(new { description = response.Description }); 
            }
        }
        [HttpGet]
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(PurchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _purchaservice.Create(model);
                if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
                return BadRequest(new { errorMessage = response.Description });
            }
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
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

        public async Task<IActionResult> Delete(long id)
        {
            var response = await _purchaservice.Delete(id);
            if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetPurchs");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<ActionResult> GetPurchData(long id, bool isJson)
        {
            var response = await _purchaservice.GetPurchData(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetPurchData", response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> GetPurchData(string term)
        {
            var response = await _purchaservice.GetPurchData(term);
            return Json(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PurchViewModel viewModel)
        {


            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                if (viewModel.Id == 0)
                {

                    await _purchaservice.Create(viewModel/* imageData*/);
                }
                else
                {
                    await _purchaservice.Edit(viewModel.Id, viewModel);
                }
            }
            else
            {
                var errorMessage = ModelState.Values
            .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
            }

            return RedirectToAction("GetPurchDatas");
        }
    }
}
