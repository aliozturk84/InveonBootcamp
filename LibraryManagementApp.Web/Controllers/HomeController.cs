using LibraryManagementApp.Web.Models;
using LibraryManagementApp.Web.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LibraryManagementApp.Web.Controllers
{
    public class HomeController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller
    {
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly UserManager<AppUser> _userManager = userManager;


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
        

        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(AppUser user, string password)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanýcý bulunamadý!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Giriþ baþarýsýz!");
                return View();
            }

            TempData["SuccessMessage"] = "Baþarýyla giriþ yaptýnýz! Hoþ geldiniz.";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            TempData["SuccessMessage"] = "Baþarýyla çýkýþ yaptýnýz.";
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            var claims = User.Claims;
            var roles = claims.Where(x => x.Type == ClaimTypes.Role).ToList();
            var userName = User.Identity.Name;
            var userId = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            ViewData["Roles"] = roles;
            ViewData["UserName"] = userName;
            ViewData["UserId"] = userId;

            return View();
        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
