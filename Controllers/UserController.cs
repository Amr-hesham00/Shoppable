using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Shoppable.Controllers;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly AppDbContext context;

    public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.context = context;
    }

    public IActionResult SignUp()
    {
        return View("signup");
    }
    [HttpPost]
    public async Task<IActionResult> SaveSignUp(SignUpVM VM)
    {
        if (ModelState.IsValid)
        {
            var appuser = new ApplicationUser
            {
                Name = VM.FullName,
                Email = VM.Email,
                UserName = VM.UserName,
                PhoneNumber = VM.Phone
            };


            IdentityResult res = await userManager.CreateAsync(appuser, VM.Password); // create in DB
            if (res.Succeeded)
            {
                //add in cust, merch DB tables
                if (VM.Role == "Merchant")
                {
                    context.Merchants.Add(new Merchant
                    {
                        Name = VM.FullName,
                        Description = "",
                        CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                        IsDeleted = false,
                        AppUserId = appuser.Id


                    });
                    context.SaveChanges();
                }
                else
                {
                    VM.Role = "Customer";
                    context.Customers.Add(new Customer
                    {
                        Name = VM.FullName,
                        Address = VM.Address,
                        City = "",
                        Phone = VM.Phone,
                        UserID = appuser.Id

                    });
                    context.SaveChanges();

                }

                // assign to role
                await userManager.AddToRoleAsync(appuser, VM.Role); // set the new user as Role

                // sign in with some claims
                if (VM.Address != null)
                {

                    List<Claim> claims = [new Claim("CustomerAddress", VM.Address)];
                    await signInManager.SignInWithClaimsAsync(appuser, VM.RememberMe, claims); // sign in with specific claims e.g. address
                }
                else
                {
                    await signInManager.SignInAsync(appuser, VM.RememberMe); // sign in with specific claims e.g. address
                }

                return RedirectToAction("Index", "Home");
            }
            foreach (var item in res.Errors) // if not
            {
                // add errors in modelstate.
                ModelState.AddModelError("", item.Description);

            }
        }
        return View("signup", VM);
    }
    public IActionResult SignIn()
    {
        return View("signin");
    }
    [HttpPost]
    public async Task<IActionResult> SaveSignInAsync(SignInVM VM)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(VM.UserName, VM.Password, VM.RememberMe, false);


            if (result.Succeeded)
            {

                var user = await userManager.FindByNameAsync(VM.UserName);

                //if (await userManager.IsInRoleAsync(user, "Admin"))
                //    return RedirectToAction("index", "Admin");

                //else if (await userManager.IsInRoleAsync(user, "Merchant"))
                //    return RedirectToAction("index", "Merchant");

                return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError("Invalid User", "UserName or Password is incorrect");
        }

        return View("signin", VM);
    }
    [HttpGet]
    public async Task<IActionResult> SignOut()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

}
