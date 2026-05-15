using Microsoft.AspNetCore.Mvc;
using Shoppable.Repositories.IRepositories;
using Shoppable.Services.IServices;
using System.Security.Claims;

namespace Shoppable.Controllers;

public class ProductController : Controller
{
    IProductService IproductService;
    IProductRepo IProductRepo;

    public ProductController(IProductService iproductService, IProductRepo iProductRepo)
    {
        IproductService = iproductService;
        IProductRepo = iProductRepo;
    }

    public async Task<IActionResult> productdetails(int id)
    {
        var product = await IproductService.ProductDetails(id);
        return View("product-detail", product);
    }
    public async Task<IActionResult> ShopProducts(ShopVM VM)
    {

        VM = await IproductService.Shop(VM);
        return View("ShopProducts", VM);
    }

    //======================== merchant actions ========================
    public IActionResult Create()
    {
        return View("CreateProduct");
    }
    [HttpPost]

    public async Task<IActionResult> SaveCreateAsync(CreateProductVM VM)
    {
        // 1. Manually remove the MerchantId error because the Service handles it
        bool IsRemoved = ModelState.Remove("MerchantId");

        if (ModelState.IsValid)
        {
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;


            await IproductService.SaveCreateAsync(VM, userid);

            return RedirectToAction("Products", "Dashboard");
        }

        return View("CreateProduct", VM);

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        Product p = await IProductRepo.GetById(id);
        return View("Edit", p);
    }

    [HttpPost]
    public async Task<IActionResult> SaveEditAsync(Product VM)
    {
        if (ModelState.IsValid)
        {

            await IproductService.SaveUpdateAsync(VM);
            return RedirectToAction("Products", "Dashboard");
        }
        return View("Edit", VM);
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
        await IproductService.SaveDeleteAsync(id);
        return RedirectToAction("Products", "Dashboard");
    }

    public async Task<IActionResult> DetailsAsync(int id)
    {
        Product p = await IProductRepo.GetById(id);
        return View("Details", p);
    }
    //=================================================================

}

