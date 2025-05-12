using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NetworkBandwidthAlgorithm.Models;

namespace NetworkBandwidthAlgorithm.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new BandwidthModel());
        }

        [HttpPost]
        public IActionResult Calculate(BandwidthModel model)
        {
            model.Calculate();
            return View("Index", model);
        }
    }
}

