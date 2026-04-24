namespace Shoppable.Services.IServices;

public interface ICartService
{
    public Task<Cart?> GetOrCreateCartAsync(string userId);
    public Task<Result> AddToCartAsync(string userId, AddToCartVM VM);
    public Task RemoveFromCartAsync(string userId, int ItemId);
    public Task ClearCartAsync(string userId);
    public Task UpdateItemAsync(int CartId, int quantity);

}
