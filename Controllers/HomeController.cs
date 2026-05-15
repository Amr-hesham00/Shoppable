using Microsoft.AspNetCore.Mvc;
using Shoppable.Services.IServices;
using System.Diagnostics;

namespace Shoppable.Controllers
{
    public class HomeController : Controller
    {
            IProductService IproductService;


            ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IProductService iproductService)
        {
            _logger = logger;
            IproductService = iproductService;
        }

        public async Task<IActionResult> index(ShopVM? VM)
        {

            VM = await IproductService.Shop(VM);

            return View("index", VM);
        }
        public IActionResult about()
        {
            return View("about");
        }

        public IActionResult contact()
        {
            return View("contact");
        }


        public IActionResult shoppingcart()
        {
            return View("shopping-cart");
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
