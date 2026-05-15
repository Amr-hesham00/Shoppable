using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shoppable.Data.UnitOfWork;
using Shoppable.Repositories.IRepositories;
using Shoppable.Services.IServices;

namespace Shoppable.Services;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;

    //IMerchantRepo IMerchantRepo;
    //IProductRepo IProductRepo;
    //IOrderItemRepo IOrderItemRepo;
    //IOrderRepo IOrderRepo;
    //ICustomerRepo ICustomerRepo;
    UserManager<ApplicationUser> userManager;


    public DashboardService(IMerchantRepo IMerchantRepo, IOrderItemRepo IOrderItemRepo, IProductRepo _productrepo, IOrderRepo iOrderRepo, ICustomerRepo iCustomerRepo, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
    {
        //this.IOrderItemRepo = IOrderItemRepo;
        //this.IMerchantRepo = IMerchantRepo;
        //IProductRepo = _productrepo;
        //IOrderRepo = iOrderRepo;
        //ICustomerRepo = iCustomerRepo;
        this.userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrdersFilterVM> Orders(string? userid, OrdersFilterVM VM)
    {
        if (userid == null) { return null; }

        int merchantId = (await _unitOfWork.Merchant.GetByUserIdAsync(userid))?.Id ?? 0; // If merchant = null, the ?? operator kicks in and assigns the default value Id = 0 
        if (merchantId == 0) { return null; }

        List<Order>? orders = await _unitOfWork.Order.OrdersbyMerchantId(merchantId); // get orders that has items for this merchantId
        orders = await _unitOfWork.OrderItem.FilterItemsByMerchantId(orders, merchantId); // filter orderitems that has items for this merchantId

        var query = orders.AsQueryable(); // enable to apply multiple filters instead of creating new list every filter( .tolist() )
        //Filter here

        // id filter
        if (VM.Id.HasValue)
        {
            query = query.Where(p => p.Id == VM.Id);
        }

        // Price filters
        if (VM.MinPrice.HasValue)
        {
            query = query.Where(p => p.TotalAmount >= VM.MinPrice.Value);
        }

        if (VM.MaxPrice.HasValue)
        {
            query = query.Where(p => p.TotalAmount <= VM.MaxPrice.Value);
        }

        // Stock Qt.
        if (VM.MinStockQuantity.HasValue)
        {
            query = query.Where(p => p.orderItems.Count >= VM.MinStockQuantity.Value);
        }

        if (VM.MaxStockQuantity.HasValue)
        {
            query = query.Where(p => p.orderItems.Count <= VM.MaxStockQuantity.Value);
        }

        // Date filters
        if (VM.createdAfter.HasValue)
        {
            query = query.Where(p => p.OrderDate >= VM.createdAfter.Value);
        }

        if (VM.CreatedBefore.HasValue)
        {
            query = query.Where(p => p.OrderDate <= VM.CreatedBefore.Value);
        }

        // Status filter
        if (VM.orderstatus.HasValue)
        {
            query = query.Where(x => x.Status == VM.orderstatus.Value);
        }

        orders = query.ToList();

        return new OrdersFilterVM
        {
            Orders = orders,
        };

    }
    public async Task<CustomersFilterVM>? Customers(string? userid, CustomersFilterVM VM)
    {

        if (userid == null) { return null; }

        int merchantId = (await _unitOfWork.Merchant.GetByUserIdAsync(userid))?.Id ?? 0; // If merchant = null, the ?? operator kicks in and assigns the default value 0 to your variable.
        if (merchantId == 0) { return null; }

        var customers = await _unitOfWork.Customer.GetByMerchantId(merchantId);

        var query = customers.AsQueryable(); // enable to apply multiple filters instead of creating new list every filter( .tolist() )

        // id filter
        if (VM.Id.HasValue)
        {
            query = query.Where(p => p.Id == VM.Id);
        }

        // Name filter
        if (!string.IsNullOrEmpty(VM.Name))
        {
            query = query.Where(p => p.Name.Contains(VM.Name!, StringComparison.OrdinalIgnoreCase));
        }

        // City filter
        if (!string.IsNullOrEmpty(VM.City))
        {
            query = query.Where(p => p.City.Contains(VM.City!, StringComparison.OrdinalIgnoreCase));
        }

        // Phone filter
        if (!string.IsNullOrEmpty(VM.Phone))
        {
            query = query.Where(p => p.Phone == VM.Phone);
        }

        customers = query.ToList();

        return new CustomersFilterVM
        {
            customers = customers
        };
    }

    public async Task<DashboardProductsVM>? Products(string? userid, ProductFilterVM VM)
    {

        if (userid == null) { return null; }

        int merchantId = (await _unitOfWork.Merchant.GetByUserIdAsync(userid))?.Id ?? 0; // If merchant = null, the ?? operator kicks in and assigns the default value 0 to your variable.
        if (merchantId == 0) { return null; }


        // here => then user is authenticated
        var products = await _unitOfWork.Product.GetByMerchantIdAsync(merchantId);

        var query = products.AsQueryable(); // enable to apply multiple filters instead of creating new list every filter( .tolist() )
        //Filter here

        // Name filter
        if (!string.IsNullOrWhiteSpace(VM.Name))
        {
            query = query.Where(p =>
                p.Name.Contains(VM.Name, StringComparison.OrdinalIgnoreCase));
        }

        // Price filters
        if (VM.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= VM.MinPrice.Value);
        }

        if (VM.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= VM.MaxPrice.Value);
        }

        // Stock Qt.
        if (VM.MinStockQuantity.HasValue)
        {
            query = query.Where(p => p.StockQuantity >= VM.MinStockQuantity.Value);
        }

        if (VM.MaxStockQuantity.HasValue)
        {
            query = query.Where(p => p.StockQuantity <= VM.MaxStockQuantity.Value);
        }

        // Date filters
        if (VM.createdAfter.HasValue)
        {
            query = query.Where(p => p.CreatedDate >= VM.createdAfter.Value);
        }

        if (VM.CreatedBefore.HasValue)
        {
            query = query.Where(p => p.CreatedDate <= VM.CreatedBefore.Value);
        }

        // Status filter
        if (!VM.Status.IsNullOrEmpty())
        {
            if (VM.Status == "true")
                query = query.Where(p => !p.IsDeleted); // Active
            else if (VM.Status == "false")
                query = query.Where(p => p.IsDeleted);  // Inactive
        }
        products = query.ToList();

        return new DashboardProductsVM
        {
            Products = products,
        };
    }
    public async Task<AccountDashboardVM> Account(string userid)
    {
        var user = userManager.Users.FirstOrDefault(u => u.Id == userid);

        if (user == null)
            return null;


        return new AccountDashboardVM
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Name = user.Name,
        };
    }
    public async Task<bool> UpdateAccount(AccountDashboardVM VM)
    {
        var user = await userManager.FindByIdAsync(VM.UserId);

        user.UserName = VM.UserName;
        user.Email = VM.Email;
        user.PhoneNumber = VM.PhoneNumber;
        user.Name = VM.Name;
        user.Email = VM.Email;


        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return false;

        return true;
    }
}

