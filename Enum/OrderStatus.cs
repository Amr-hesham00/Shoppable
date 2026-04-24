namespace Shoppable.Enum;

public enum OrderStatus
{
    PendingPayment,
    AwaitingVerification,
    Verified,
    Rejected,
    Shipped,
    Delivered,
    Cancelled,
    Refunded
}