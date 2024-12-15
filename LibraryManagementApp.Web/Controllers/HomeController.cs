using LibraryManagementApp.Web.Models;
using LibraryManagementApp.Web.Models.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LibraryManagementApp.Web.Controllers
{
    public class HomeController(
       UserManager<AppUser> userManager,
       RoleManager<AppRole> roleManager,
       SignInManager<AppUser> signInManager) : Controller
    {
       
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            var email = "drazing84@outlook.com";
            var password = "Drazing84*";


            var hasUser = await userManager.FindByEmailAsync(email);


            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya sifre yanlis");
            }

            var passwordCheck = await userManager.CheckPasswordAsync(hasUser, password);

            if (!passwordCheck)
            {
                ModelState.AddModelError(string.Empty, "Email veya sifre yanlis");
            }


            await signInManager.SignInAsync(hasUser, new AuthenticationProperties() { IsPersistent = true });


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddClaimToUser()
        {
            var user = await userManager.FindByNameAsync("drazing84");

            await userManager.AddClaimAsync(user,
                new Claim("city", "istanbul"));


            userManager.UpdateSecurityStampAsync(user);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddRoleToUser()
        {
            var newRole = new AppRole()
            {
                Name = "Admin"
            };

            var response = await roleManager.CreateAsync(newRole);


            var user = await userManager.FindByNameAsync("drazing84");


            await userManager.AddToRoleAsync(user, "Admin");


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UserCreate()
        {
            var appUse = new AppUser()
            {
                UserName = "drazing84",
                Email = "drazing84@outlook.com",
            };


            var identityResult = await userManager.CreateAsync(appUse, "Drazing84*");


            if (!identityResult.Succeeded)
            {
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {

            var claims = User.Claims;

            var roles = claims.Where(x => x.Type == ClaimTypes.Role).ToList();


            var userName = User.Identity.Name;
            var userId = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return View();
        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
