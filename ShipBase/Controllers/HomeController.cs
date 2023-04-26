using ShipBase.Domain.Entity;
using ShipBase.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ShipBase.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

    }
}