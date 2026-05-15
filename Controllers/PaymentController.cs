using Microsoft.AspNetCore.Mvc;
using Shoppable.Services.IServices;

namespace Shoppable.Controllers;

public class PaymentController : Controller
{
      IPaymentService IPaymentService;

    public PaymentController(IPaymentService iPaymentService)
    {
        IPaymentService = iPaymentService;
    }

    public async Task<IActionResult> Index(PaymentVM VM)
    {
        PaymentVM payment = await IPaymentService.SetPaymentVM(VM.CartId);
        payment.Total = VM.Total;

        return View("Index", payment);
    }
}
