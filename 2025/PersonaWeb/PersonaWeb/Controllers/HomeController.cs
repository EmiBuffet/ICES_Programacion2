using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonaWeb.Models;
using Prometheus;

namespace PersonaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly Counter ContadorPeticiones = Metrics.CreateCounter("total_peticiones_privacy", "Total de peticiones procesadas.");

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ContadorPeticiones.Inc();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
