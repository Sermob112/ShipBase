using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipBase.Domain.SectionOne.Extensions;
using ShipBase.Domain.SectionOne.ViewModels.Customer;
using ShipBase.Domain.SectionOne.ViewModels.PurchasingData;
using ShipBase.Service.SectionOne.Interfaces;
using System.Linq;

namespace ShipBase.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class PurchaseController : Controller
    {
        private readonly IPurchasingDataService _purchasingDataService;

        public PurchaseController(IPurchasingDataService purchasingDataService)
        {
            _purchasingDataService = purchasingDataService;
        }
        [HttpGet]
        public IActionResult GetPurchasingDatas()
        {
            var response = _purchasingDataService.GetPurchasingDatas();
            if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _purchasingDataService.Delete(id);
            if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetPurchasingDatas");
            }
            return View("Error", $"{response.Description}");
        }
        public IActionResult Compare() => PartialView();

        [HttpGet]
        public async Task<IActionResult> Save(long id)
        {
            if (id == 0)
                return PartialView();

            var response = await _purchasingDataService.GetPurchasingData(id);
            if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(PurchasingDataViewModel viewModel)
        {
         
              
            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                if (viewModel.Id == 0)
                {
                    
                    await _purchasingDataService.Create(viewModel/* imageData*/);
                }
                else
                {
                    await _purchasingDataService.Edit(viewModel.Id, viewModel);
                }
            }
            else
            {
                var errorMessage = ModelState.Values
            .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
            }

            return RedirectToAction("GetPurchasingData");
        }
        [HttpGet]
        public async Task<ActionResult> GetPurchasingData(long id, bool isJson)
        {
            var response = await _purchasingDataService.GetPurchasingData(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetPurchasingData", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetPurchaseData(string term)
        {
            var response = await _purchasingDataService.GetPurchasingData(term);
            return Json(response.Data);
        }

    }
}
