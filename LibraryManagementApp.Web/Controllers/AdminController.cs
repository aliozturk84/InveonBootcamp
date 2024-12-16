using LibraryManagementApp.Web.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;


        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı silinemedi.");
            }

            return RedirectToAction("Index");
        }
    }
}