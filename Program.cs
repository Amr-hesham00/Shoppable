using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shoppable.Data.UnitOfWork;
using Shoppable.Repositories;
using Shoppable.Repositories.Generic;
using Shoppable.Repositories.IRepositories;
using Shoppable.Services;
using Shoppable.Services.IServices;

/*
==== Home page ====
* cart icon number in nav bar
* Serch icon in nav bar
* load more button
* add more sections
* products section(filter, search buttons)
* vodafone,instapay icons
* footer
* add account settings page
* add customer orders page
===================

 */

var builder = WebApplication.CreateBuilder(args);
//----------------------------------------------------------------------------
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredLength = 3;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    }
    ).AddEntityFrameworkStores<AppDbContext>();


//--------------------------------------Repositories--------------------------------------

builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>)); // inject the generic repository
builder.Services.AddScoped<IProductRepo, ProductRepo>(); // inject Product repository
builder.Services.AddScoped<IMerchantRepo, MerchantRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICartItemRepo, CartItemRepo>();
builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // UoW

//--------------------------------------services--------------------------------------

builder.Services.AddScoped<IDashboardService, DashboardService>(); // inject Dash service
builder.Services.AddScoped<IProductService, ProductService>(); // inject Product service
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
//builder.Services.AddScoped<ICartItemService, CartItemService>();
//builder.Services.AddScoped<IOrderItemService, OrderItemService>();
//builder.Services.AddScoped<IMerchantService, MerchantService>();



//----------------------------------------------------------------------------
var app = builder.Build(); //Unable to resolve service for type 'AppDbContext' while attempting to activate 

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = ["Admin", "Customer", "Merchant"];

    // Create Roles if not exist
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    // Create Admin user if not exist
    string adminUserName = "admin@shoppable.com";

    var adminUser = await userManager.FindByNameAsync(adminUserName);

    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "admin@shoppable.com",
            //Email = adminUserName,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(adminUser, "Admin@123");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapStaticAssets();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//.WithStaticAssets();


app.Run();
