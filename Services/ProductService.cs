using Shoppable.Enum;
using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;

namespace Shoppable.Services.IServices;

public class ProductService : GenericRepo<Product>, IProductService
{
    private readonly IMerchantRepo IMerchantRepo;
    private readonly IProductRepo IProductRepo;

    public ProductService(AppDbContext context, IMerchantRepo IMerchantRepo, IProductRepo iProductRepo) : base(context)
    {
        this.IMerchantRepo = IMerchantRepo;
        IProductRepo = iProductRepo;
    }

    public async Task<Product?> ProductDetails(int id)
    {
        return await IProductRepo.GetById(id);
    }

    public async Task SaveCreateAsync(CreateProductVM VM, string userid)
    {
        Merchant m = await IMerchantRepo.GetByUserIdAsync(userid);

        Product p = new Product
        {
            MerchantId = m.Id,
            CreatedDate = DateOnly.FromDateTime(DateTime.Now),
            Name = VM.Name,
            category = VM.category,
            Description = VM.Description,
            Price = VM.Price,
            StockQuantity = VM.StockQuantity,
            Colors = VM.Colors.Split(',').Select(c => c.Trim()).ToList(), // "blue ,red " => "blue ," => "blue"
            Sizes = VM.Sizes.Split(',').Select(c => c.Trim()).ToList()
        };

        if (VM.image != null)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(VM.image.FileName); // unique file name (Avoid overwriting files): 7a308ca8bb26.jpg  
            string folderPath = Path.Combine("wwwroot", "images", "Products"); // products path = "wwwroot/images/Products"

            if (!Directory.Exists(folderPath)) // if folder not exist, then create one
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName); // ex: wwwroot/images/Products/3f8c2a1e.png

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await VM.image.CopyToAsync(stream);

            }
            /*
             FileStream: opens a file for writing
             FileMode.Create: creates file if not exists, overwrites if exists
             CopyToAsync(stream): copies uploaded file data into your server file
            */

            p.ImageUrl = $"/images/Products/{fileName}"; // the url will be opened when needed to display the image.
        }
        await IProductRepo.CreateAsync(p);
        await IProductRepo.SaveAsync();
    }
    public async Task SaveDeleteAsync(int id)
    {
        Product p = await IProductRepo.GetById(id);
        p.IsDeleted = p.IsDeleted == true ? false : true;
        await IProductRepo.SaveAsync();
    }

    public async Task SaveUpdateAsync(Product VM)
    {
        Product p = await IProductRepo.GetById(VM.Id);
        p.StockQuantity = VM.StockQuantity;
        p.Price = VM.Price;
        p.CreatedDate = VM.CreatedDate;
        p.Description = VM.Description;
        p.ImageUrl = VM.ImageUrl;
        p.Name = VM.Name;

        IProductRepo.Update(p);
        await IProductRepo.SaveAsync();


    }

    public async Task<ShopVM> Shop(ShopVM VM)
    {

        var products = await IProductRepo.GetAllAsync(new Product());

        var query = products.AsQueryable();

        query = query.Where(p => !p.IsDeleted);

        if (!string.IsNullOrWhiteSpace(VM.Search))
        {
            query = query
                .Where(p => p.Name.Contains(VM.Search, StringComparison.OrdinalIgnoreCase));
        }

        if (VM.Minprice.HasValue)
        {
            query = query
                .Where(p => p.Price >= VM.Minprice.Value);
        }

        if (VM.Maxprice.HasValue)
        {
            query = query.Where(p => p.Price <= VM.Maxprice.Value);
        }

        if (VM.category != Category.All)
        {
            query = query.Where(p => p.category == VM.category);
        }

        VM.Products = query.ToList();

        return VM;

    }


}
