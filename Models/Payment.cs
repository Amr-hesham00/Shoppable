using Shoppable.Enum;

namespace Shoppable.Models;

public class Payment
{

    public int Id { get; set; }
    public PaymentMethod PaymentMethod { set; get; }
    public PaymentStatus PaymentStatus { set; get; }
    public string ScreenshotPath { set; get; }

    public int OrderId { get; set; }
    public Order order { get; set; }


    //   TransactionReference → proves payment happened
    //   VerifiedByAdminId → who approved it
    //   VerifiedDate → when it was approved
}

