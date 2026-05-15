using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoppable.Services.IServices;
using System.Security.Claims;

namespace Shoppable.Controllers;

[Authorize(Roles = "Admin, Merchant")]
public class DashboardController : Controller
{

        IDashboardService dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        this.dashboardService = dashboardService;
    }

    [HttpGet]
    public ActionResult Index()
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


        //int merchantid = IMerchantRepo.GetByUserId(userId);

        //if (merchantid == 0)
        //{
        //    return Content("Unauthorized");
        //}

        return View("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Orders(OrdersFilterVM VM)
    {
        string? userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //@ViewData["CurrentFilter"] = VM.Name;

        var data = await dashboardService.Orders(userid, VM);

        return View("Orders", data);
    }

    [HttpGet]
    public async Task<IActionResult> Customers(CustomersFilterVM VM)
    {
        string? userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        @ViewData["CurrentFilter"] = VM.Name;

        var data = await dashboardService.Customers(userid, VM);

        return View("Customers", data);
    }

    [HttpGet]
    public async Task<IActionResult> Products(ProductFilterVM VM)
    {
        string? userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

        @ViewData["CurrentFilter"] = VM.Name;

        DashboardProductsVM? data = await dashboardService.Products(userid, VM);

        return View("Products", data);
    }

    [HttpGet]
    public async Task<IActionResult> Account(AccountDashboardVM VM)
    {

        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        AccountDashboardVM? data = await dashboardService.Account(userId!);

        if (data == null)
        {
            return NotFound();
        }

        return View("Account", data);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateAccount(AccountDashboardVM vm)
    {
        if (ModelState.IsValid)
        {

            bool updated = await dashboardService.UpdateAccount(vm);

            if (updated)
            {
                return RedirectToAction("Account", vm);
            }

            ModelState.AddModelError("", "Something went wrong in update process, check your inputs");
            return View(vm);
        }
        return View(vm);
    }
}

