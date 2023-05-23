using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Service.SectionOne.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ShipBase.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

    }
}