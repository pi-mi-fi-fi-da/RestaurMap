using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurMap.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RestaurMap.Controllers;

public class AccountController : Controller
{
    private UserManager<ApplicationUser> userManager;
    private SignInManager<ApplicationUser> signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password )
    {
        if (ModelState.IsValid)
        {
            ApplicationUser appUser = await userManager.FindByEmailAsync(email);
            if (appUser != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, password, false, false);
                if (result.Succeeded)
                {
                    return Redirect("https://localhost:7143");
                }
            }
            ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or Password");
        }

        return View();
    }
    // login and logout actions 
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
