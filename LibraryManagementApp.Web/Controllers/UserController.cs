using LibraryManagementApp.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;


        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userRoles = new Dictionary<Guid, List<string>>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles.ToList();
            }

            ViewBag.UserRoles = userRoles;

            return View(users);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // Kullanıcı Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                ViewBag.SuccessMessage = "Kullanıcı başarıyla eklendi!";
                return View(); // Aynı sayfada kal ve mesaj göster.
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }


        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(p => p.Id == id);
            return View(user); // Tek bir kullanıcıyı döndür
        }


        // Kullanıcı Güncelleme
        [HttpPost]
        public async Task<IActionResult> Update(AppUser user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id.ToString());
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            // Diğer gerekli alanlar

            var result = await _userManager.UpdateAsync(existingUser);
            if (result.Succeeded)
            {
                ViewBag.SuccessMessage = "Kullanıcı bilgileri başarıyla güncellendi!";
                return View(existingUser); // Aynı sayfada kal ve mesajı göster
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }


        // Kullanıcı Silme
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Kullanıcı başarıyla silindi!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Kullanıcı silinemedi.");
            return RedirectToAction("Index");
        }
    }
}
