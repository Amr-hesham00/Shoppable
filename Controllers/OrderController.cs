using Microsoft.AspNetCore.Mvc;
using Shoppable.Repositories.IRepositories;
using Shoppable.Services.IServices;
using System.Security.Claims;

namespace Shoppable.Controllers;

public class OrderController : Controller
{
    IOrderService IOrderService;
    IOrderRepo IOrderRepo;
    ICustomerRepo ICustomerRepo;

    public OrderController(IOrderRepo iOrderRepo, IOrderService iOrderService, ICustomerRepo iCustomerRepo)
    {
        IOrderRepo = iOrderRepo;
        IOrderService = iOrderService;
        ICustomerRepo = iCustomerRepo;
    }


    //--------------------------------vendor actions--------------------------------
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {

        OrderVM VM = await IOrderService.GetOrderVM(id);
        return View("Edit", VM);
    }

    [HttpPost]
    public async Task<IActionResult> SaveEditAsync(OrderVM VM)
    {
        if (ModelState.IsValid)
        {
            await IOrderService.SaveUpdateAsync(VM);
            return RedirectToAction("Orders", "Dashboard");
        }
        return RedirectToAction("Edit", new { id = VM.Id });
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
    //--------------------------------customer actions--------------------------------

    [HttpGet]
    public async Task<IActionResult> Orders()
    {

        string? userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userid == null)
            return NotFound();

        var OrdersVM = await IOrderService.GetAllOrders(userid);

        if (OrdersVM == null)
            return NotFound();

        return View("Orders", OrdersVM);
    }

    [HttpGet]
    public async Task<IActionResult> OrderDetails(int orderid)
    {
        var order = await IOrderRepo.Order_byId_WithItems(orderid);

        return View("OrderDetails", order);
    }

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
