using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ShipBase.Domain.SectionOne.Extensions;
using ShipBase.Domain.SectionOne.ViewModels.Customer;
using ShipBase.Service.SectionOne.Interfaces;

namespace ShipBase.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class CustomerController : Controller
    {
       
       
    private readonly ICustomerService _castService;

    public CustomerController(ICustomerService castService)
    {
        _castService = castService;
    }
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var response = _castService.GetCustomers();
            if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _castService.DeleteCustomer(id);
            if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetCustomers");
            }
            return View("Error", $"{response.Description}");
        }
      

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return PartialView();

            var response = await _castService.GetCustomer(id);
            if (response.StatusCode == Domain.SectionOne.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(СustomerViewModel viewModel)
        {
            /* await _carService.Edit(viewModel.Id, viewModel);
             return RedirectToAction("GetCars"); */
            /*     ModelState.Remove("Id");
                 ModelState.Remove("DateCreate");*/
            ModelState.Remove("Id");
           
            if (ModelState.IsValid)
            {
                if (viewModel.Id == 0)
                {
                    /*byte[] imageData;
                    using (var binaryReader = new BinaryReader(viewModel.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)viewModel.Avatar.Length);
                    }*/
                    await _castService.Create(viewModel/* imageData*/);
                }
                else
                {
                    await _castService.Edit(viewModel.Id, viewModel);
                }
            }
            else
            {
                var errorMessage = ModelState.Values
            .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
            }

            return RedirectToAction("GetCustomers");
        }
        [HttpGet]
        public async Task<ActionResult> GetCustomer(int id, bool isJson)
        {
            var response = await _castService.GetCustomer(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetCustomer", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomer(string term)
        {
            var response = await _castService.GetCustomer(term);
            return Json(response.Data);
        }

    }
}
