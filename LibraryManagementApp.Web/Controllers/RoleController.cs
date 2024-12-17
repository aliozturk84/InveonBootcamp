using LibraryManagementApp.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager; 

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager; 
        }


        // Roller Listeleme
        public IActionResult Index()
        {

            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UserRoles()
        {
            var users = _userManager.Users.ToList(); // Tüm kullanıcıları getir
            var userRoles = new Dictionary<AppUser, List<string>>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); // Kullanıcı rolleri
                userRoles.Add(user, roles.ToList());
            }

            return View(userRoles); // View'e taşı
        }


        // Rol Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var result = await _roleManager.CreateAsync(new AppRole { Name = name });
            if (result.Succeeded)
            {
                ViewBag.SuccessMessage = "Rol başarıyla eklendi!";
                return View();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }


        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var role = _roleManager.Roles.FirstOrDefault(p => p.Id == id);
            return View(role); // Tek bir rolü döndürür
        }


        // Rol Güncelleme
        [HttpPost]
        public async Task<IActionResult> Update(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                ViewBag.SuccessMessage = "Rol başarıyla güncellendi!";
                return View(role); // Aynı sayfada kal ve başarı mesajını göster
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(role);
        }


        // Rol Silme
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Rol başarıyla silindi!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Rol silinemedi.");
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> AssignRole(string userId)
        {
            // Kullanıcıyı bul
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // Kullanıcının mevcut rollerini al
            var userRoles = await _userManager.GetRolesAsync(user);

            // Tüm rollerin listesini al
            var allRoles = _roleManager.Roles.ToList();

            // ViewBag üzerinden rolleri view'a gönder
            ViewBag.UserId = user.Id;
            ViewBag.UserName = user.UserName;
            ViewBag.AllRoles = allRoles;
            ViewBag.UserRoles = userRoles;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                ViewBag.SuccessMessage = $"{roleName} rolü başarıyla atandı.";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Rol atanırken bir hata oluştu.");
            }

            // Sayfayı tekrar yüklemek için gerekli verileri doldur
            var allRoles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            ViewBag.UserId = user.Id;
            ViewBag.UserName = user.UserName;
            ViewBag.AllRoles = allRoles;
            ViewBag.UserRoles = userRoles;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                ViewBag.SuccessMessage = $"{roleName} rolü başarıyla kaldırıldı.";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Rol kaldırılamadı.");
            }

            // Sayfayı tekrar yüklemek için gerekli veriler
            var allRoles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            ViewBag.UserId = user.Id;
            ViewBag.UserName = user.UserName;
            ViewBag.AllRoles = allRoles;
            ViewBag.UserRoles = userRoles;

            return View("AssignRole"); // AssignRole sayfasını tekrar yükler
        }
    }
}

