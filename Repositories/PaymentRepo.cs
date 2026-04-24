using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;

namespace Shoppable.Repositories;

public class PaymentRepo : GenericRepo<Payment>, IPaymentRepo
{
    public PaymentRepo(AppDbContext context) : base(context)
    {
    }
}
