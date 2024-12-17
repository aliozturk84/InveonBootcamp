using LibraryManagementApp.Web.Models.Entities;
using LibraryManagementApp.Web.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApp.Web.Controllers
{
    [Authorize(Roles = "Admin,Ziyaretci")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // Kitap Listeleme
        public async Task<IActionResult> Index()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return View(books);
        }


        // Kitap Detayları
        public async Task<IActionResult> Details(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }


        // Kitap Ekleme (GET)
        public IActionResult Create()
        {
            return View();
        }


        // Kitap Ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Books.AddAsync(book);
                await _unitOfWork.CompleteAsync();
                TempData["SuccessMessage"] = "Kitap başarıyla eklendi!";
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }


        // Kitap Güncelleme (GET)
        public async Task<IActionResult> Update(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }


        // Kitap Güncelleme (POST)
        [HttpPost]
        public async Task<IActionResult> Update(Book book)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Books.Update(book);
                await _unitOfWork.CompleteAsync();
                TempData["SuccessMessage"] = "Kitap başarıyla güncellendi!";
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }


        // Kitap Silme
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) return NotFound();

            _unitOfWork.Books.Delete(book);
            await _unitOfWork.CompleteAsync();
            TempData["SuccessMessage"] = "Kitap başarıyla silindi!";
            return RedirectToAction(nameof(Index));
        }


        // Kitap Arama
        public async Task<IActionResult> Search(string searchText)
        {
            // Eğer arama metni boşsa ara dediğimizde tüm kitapları getirir
            if (string.IsNullOrWhiteSpace(searchText))
            {
                var allBooks = await _unitOfWork.Books.GetAllAsync();
                return View("Index", allBooks);
            }
            var books = await _unitOfWork.Books.SearchBooksAsync(searchText);
            return View("Index", books);
        }
    }

}
