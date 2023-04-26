using System;
using System.Linq;
using System.Threading.Tasks;
using ShipBase.Domain.Extensions;
using ShipBase.Domain.ViewModels.Profile;
using ShipBase.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ShipBase.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProfileViewModel model)
        {
            ModelState.Remove("Id");
            ModelState.Remove("UserName");
            if (ModelState.IsValid)
            {
                var response = await _profileService.Save(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            else
            {
                var errorMessage = ModelState.Values
         .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
            }
            return View(model);
            
        }
        
        public async Task<IActionResult> Detail()
        {
            var userName = User.Identity.Name;
            var response = await _profileService.GetProfile(userName);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);   
            }
            return View();
        }
    }
}