namespace Shoppable.Services.IServices;

public interface IProductService
{
    public Task SaveCreateAsync(CreateProductVM VM, string userid);
    public Task SaveUpdateAsync(Product p);
    public Task SaveDeleteAsync(int id);
    public Task<ShopVM> Shop(ShopVM VM);
    public Task<Product?> ProductDetails(int id);


}
