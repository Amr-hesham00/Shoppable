using Microsoft.AspNetCore.Mvc;

namespace Shoppable.Controllers;
public class MerchantController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
