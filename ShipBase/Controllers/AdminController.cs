using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ShipBase.Domain.Extensions;
using ShipBase.Domain.ViewModels.User;
using ShipBase.Service.Implementations;
using ShipBase.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipBase.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;

        }

        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }
       

       

        public async Task<IActionResult> DeleteUser(long id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Save() => PartialView();
        public IActionResult SaveOrder() => PartialView();
        /* public IActionResult Update() => PartialView();*/


      
       

      
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
                return PartialView();

            var response = await _userService.GetUser(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
                return BadRequest(new { errorMessage = response.Description });
            }
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel model)
        {
          /*  ModelState.Remove("Id");*/
            
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _userService.Create(model);
                }
                else
                {
                    await _userService.Edit(model.Id, model);
                }
            }
            return RedirectToAction("GetUsers");
        }
       

        [HttpPost]
        public JsonResult GetRoles()
        {
            var types = _userService.GetRoles();
            return Json(types.Data);
        }

      
    }
}