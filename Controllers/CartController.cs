using Microsoft.AspNetCore.Mvc;
using Shoppable.Services.IServices;
using System.Security.Claims;

namespace Shoppable.Controllers;

public class CartController : Controller
{
        ICartService ICartService;

    public CartController(ICartService _carts)
    {
        ICartService = _carts;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            TempData["not a customer"] = "You must be a customer";
            return RedirectToAction("SignUp", "User");
        }

        Cart? cart = await ICartService.GetOrCreateCartAsync(userId); // if user not a customer it will return null

        if (cart == null)
        {
            TempData["not a customer"] = "You must be a customer";
            return RedirectToAction("SignUp", "User");
        }

        return View("Index", cart);
    }

    [HttpPost]
    public async Task<IActionResult> Update(CartVM VM)
    {
        foreach (var item in VM.cartitems)
        {
            await ICartService.UpdateItemAsync(item.Id, item.Quantity);
        }

        return RedirectToAction("index", "cart");
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddToCartVM VM)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            TempData["not a customer"] = "You must be a customer";
            return RedirectToAction("SignUp", "User");
        }
        if (ModelState.IsValid)
        {


            if (userId == null)
                return Unauthorized();

            Result res = await ICartService.AddToCartAsync(userId, VM);

            if (res.Success)
                return RedirectToAction("Index");

            ModelState.AddModelError("", res.Message);
        }
        return RedirectToAction("productdetails", "product", new { id = VM.ProductId }); // will return to product-details view page
    }

    [HttpGet]
    public async Task<IActionResult> Remove(int itemId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        await ICartService.RemoveFromCartAsync(userId, itemId);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Clear()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        await ICartService.ClearCartAsync(userId);

        return RedirectToAction("Index");
    }

}
