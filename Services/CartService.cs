using Shoppable.Data.UnitOfWork;
using Shoppable.Repositories.IRepositories;
using Shoppable.Services.IServices;

namespace Shoppable.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;

    //ICartRepo ICartRepo;
    //IProductRepo IProductRepo;
    //ICustomerRepo ICustomerRepo;
    //ICartItemRepo ICartItemRepo;
    //    UserManager<ApplicationUser> userManager;
    //    IMerchantRepo IMerchantRepo;
    //    IOrderItemRepo IOrderItemRepo;
    //    IOrderRepo IOrderRepo;

    public CartService(ICartRepo cartRepo, /*ICartItemRepo iCartItemRepo, UserManager<ApplicationUser> userManager,*/ ICustomerRepo iCustomerRepo, IProductRepo iProductRepo, ICartItemRepo iCartItemRepo, IUnitOfWork unitOfWork)
    {
        //ICartRepo = cartRepo;
        //ICartItemRepo = iCartItemRepo;
        //this.userManager = userManager;
        //ICustomerRepo = iCustomerRepo;
        //IProductRepo = iProductRepo;
        //ICartItemRepo = iCartItemRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Cart?> GetOrCreateCartAsync(string userId)
    {
        var cart = await _unitOfWork.Cart.GetByUserId(userId);


        if (cart == null)
        {
            var customer = await _unitOfWork.Customer.GetByUserId(userId);

            if (customer == null)
                return null;

            cart = new Cart
            {
                CustomerId = customer.Id,
                UserId = userId,
                CreatedDate = DateOnly.FromDateTime(DateTime.Now),

            };

            await _unitOfWork.Cart.CreateAsync(cart);
            await _unitOfWork.Cart.SaveAsync();
        }

        return cart;
    }
    public async Task<Result> AddToCartAsync(string userId, AddToCartVM VM)
    {
        var cart = await GetOrCreateCartAsync(userId);

        if (cart == null)
        {
            return Result.Fail("Not a Customer");
        }

        var product = await _unitOfWork.Product.GetById(VM.ProductId);

        // if item already exists 
        var existingItem = cart.cartitems.FirstOrDefault(i =>
            i.ProductId == VM.ProductId &&
            //i.product != null &&
            i.Color == VM.Color &&
            i.Size == VM.Size
        );


        if (existingItem != null)
        {
            existingItem.Quantity += VM.Quantity;
        }
        else
        {
            if (VM.Quantity > product.StockQuantity)
            {
                return Result.Fail("Out of stock");
            }
            if (!product.Sizes.Contains(VM.Size))
            {
                return Result.Fail("Size Not Found");

            }
            if (!product.Colors.Contains(VM.Color))
            {
                return Result.Fail("Color Not Found");
            }


            CartItem newitem = new CartItem
            {
                Quantity = VM.Quantity,
                ProductId = VM.ProductId,
                CartId = cart.Id,
                Color = VM.Color,
                Size = VM.Size,
            };

            cart.cartitems.Add(newitem);
        }


        _unitOfWork.Cart.Update(cart);
        await _unitOfWork.Cart.SaveAsync();

        return Result.Ok("Item Added Successfully");

    }

    public async Task ClearCartAsync(string userId)
    {
        var cart = await GetOrCreateCartAsync(userId);

        cart.cartitems.Clear();

        _unitOfWork.Cart.Update(cart);
        await _unitOfWork.Cart.SaveAsync();
    }

    public async Task RemoveFromCartAsync(string userId, int ItemId)
    {
        var cart = await GetOrCreateCartAsync(userId);

        CartItem cartitem = cart.cartitems.FirstOrDefault(x => x.Id == ItemId);

        cart.cartitems.Remove(cartitem);

        _unitOfWork.Cart.Update(cart);
        await _unitOfWork.Cart.SaveAsync();
    }

    public async Task UpdateItemAsync(int itemId, int quantity)
    {
        var item = await _unitOfWork.CartItem.GetById(itemId);
        item.Quantity = quantity;

        _unitOfWork.CartItem.Update(item);
        await _unitOfWork.CartItem.SaveAsync();
    }
}
