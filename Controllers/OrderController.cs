using Microsoft.AspNetCore.Mvc;
using Shoppable.Repositories.IRepositories;
using Shoppable.Services.IServices;
using System.Security.Claims;

namespace Shoppable.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService IOrderService;
    private readonly IOrderRepo IOrderRepo;

    public OrderController(IOrderRepo iOrderRepo, IOrderService iOrderService)
    {
        IOrderRepo = iOrderRepo;
        IOrderService = iOrderService;
    }


    //--------------------------------vendor actions--------------------------------
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        Order? p = await IOrderRepo.Order_byId_WithItems(id);
        return View("Edit", p);
    }

    [HttpPost]
    public async Task<IActionResult> SaveEditAsync(Order VM)
    {
        if (ModelState.IsValid)
        {

            await IOrderService.SaveUpdateAsync(VM);
            return RedirectToAction("Orders", "Dashboard");
        }
        return RedirectToAction("Edit", VM.Id);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await IOrderService.SaveDeleteAsync(id);
        return RedirectToAction("Orders", "Dashboard");
    }

    [HttpGet]
    public async Task<IActionResult> DetailsAsync(int id)
    {
        Order? p = await IOrderRepo.Order_byId_WithItems(id);
        return View("Details", p);
    }
    //-------------------------------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PaymentVM VM)
    {
        if (ModelState.IsValid)
        {
            string? userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userid == null)
            {
                TempData["not a customer"] = "You must be a customer";
                return RedirectToAction("SignUp", "User");
            }

            bool result = await IOrderService.PlaceOrder(VM, userid);

            if (result)
            {
                TempData["SuccessfulOrder"] = "Order Made Successfully";
                return RedirectToAction("index", "Home"); // successful order
            }
            TempData["not a customer"] = "You must be a customer";
            return RedirectToAction("SignUp", "User");
        }

        return RedirectToAction("index", "payment", VM);
    }
}
