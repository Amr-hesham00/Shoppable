namespace Shoppable.Services.IServices;

public interface IDashboardService
{
    public Task<DashboardProductsVM> Products(string? userid, ProductFilterVM VM);
    public Task<OrdersFilterVM> Orders(string? userid, OrdersFilterVM VM);
    public Task<CustomersFilterVM> Customers(string? userid, CustomersFilterVM vM);
    public Task<AccountDashboardVM> Account(string userid);
    public Task<bool> UpdateAccount(AccountDashboardVM VM);
}
