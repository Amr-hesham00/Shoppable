using Shoppable.Data.UnitOfWork;
using Shoppable.Repositories.IRepositories;

namespace Shoppable.Services.IServices;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    //ICustomerRepo ICustomerRepo;
    //    ICartRepo ICartRepo;
    //    IProductRepo IProductRepo;
    //    ICartItemRepo ICartItemRepo;
    //    UserManager<ApplicationUser> userManager;
    //    IMerchantRepo IMerchantRepo;
    //    IOrderItemRepo IOrderItemRepo;
    //    IOrderRepo IOrderRepo;
    public CustomerService(ICustomerRepo iCustomerRepo, IUnitOfWork unitOfWork)
    {
        //ICustomerRepo = iCustomerRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerVM> DetailsAsync(int id)
    {
        var customer = await _unitOfWork.Customer.GetById_With_Orders(id);

        return new CustomerVM()
        {
            Address = customer.Address,
            City = customer.City,
            Id = customer.Id,
            Name = customer.Name,
            Phone = customer.Phone,
            orders = customer.orders
        };
    }

}
