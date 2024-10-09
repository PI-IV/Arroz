using ARROZ.Models;
using ARROZ.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ARROZ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommon _common;

        public HomeController(ILogger<HomeController> logger, ICommon common)
        {
            _logger = logger;
            _common = common;
        }
        
        public IActionResult Index()
        {
            // ID = 1 => Consumo Médio
            // ID = 2 => Preço Arroz
            // ID = 3 => Salário Mínimo
            // ID = 4 => Preço Dólar
            List<Common> list = new List<Common>();
            list = _common.GetConsumoMedio();
            list.AddRange(_common.GetPrecoArroz());
            list.AddRange(_common.GetSalarioMinimo());
            list.AddRange(_common.GetPrecoDolar());
            return View(list);
        }
        
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}