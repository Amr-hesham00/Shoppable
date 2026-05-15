using Microsoft.AspNetCore.Mvc;
using Shoppable.Services.IServices;

namespace Shoppable.Controllers;

public class CustomerController : Controller
{
      ICustomerService ICustomerService;

    public CustomerController(ICustomerService customerService)
    {
        this.ICustomerService = customerService;
    }

    //--------------------------------vendor actions--------------------------------

    //[HttpGet]
    //public async Task<IActionResult> DeleteAsync(int id)
    //{
    //    await customerService.SaveDeleteAsync(id);
    //    return RedirectToAction("Customers", "Dashboard");
    //}

    [HttpGet]
    public async Task<IActionResult> DetailsAsync(int id)
    {
        CustomerVM VM = await ICustomerService.DetailsAsync(id);
        return View("Details", VM);
    }
    //-------------------------------------------------------------------------------
}
