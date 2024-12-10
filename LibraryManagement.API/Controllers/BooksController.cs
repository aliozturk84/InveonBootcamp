using LibraryManagement.API.Common.Results;
using LibraryManagement.API.Models;
using LibraryManagement.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(ICacheService cacheService) : ControllerBase
    {

        private static readonly List<Book> Books = new()
        {
            new Book { Id = 1, Title = "The Great Gatsby", AuthorId = 1, CategoryId = 1, StockQuantity = 10 },
            new Book { Id = 2, Title = "1984", AuthorId = 2, CategoryId = 2, StockQuantity = 15 },
            new Book { Id = 3, Title = "To Kill a Mockingbird", AuthorId = 3, CategoryId = 3, StockQuantity = 8 },
            new Book { Id = 4, Title = "Pride and Prejudice", AuthorId = 4, CategoryId = 4, StockQuantity = 20 },
            new Book { Id = 5, Title = "Moby Dick", AuthorId = 5, CategoryId = 1, StockQuantity = 12 },
            new Book { Id = 6, Title = "War and Peace", AuthorId = 6, CategoryId = 2, StockQuantity = 5 },
            new Book { Id = 7, Title = "Crime and Punishment", AuthorId = 7, CategoryId = 3, StockQuantity = 7 },
            new Book { Id = 8, Title = "The Odyssey", AuthorId = 8, CategoryId = 4, StockQuantity = 18 },
            new Book { Id = 9, Title = "Ulysses", AuthorId = 9, CategoryId = 1, StockQuantity = 9 },
            new Book { Id = 10, Title = "Don Quixote", AuthorId = 10, CategoryId = 2, StockQuantity = 11 }
        };


        [HttpGet]
        public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken)
        {
            const string cacheKey = "all_books";

            // Cache kontrolü
            var cachedBooks = await cacheService.GetAsync<List<Book>>(cacheKey, cancellationToken);
            if (cachedBooks != null)
            {
                return Ok(ServiceResult<List<Book>>.Success(cachedBooks, "Kitaplar önbellekten başarıyla getirildi."));
            }

            // Önbellekte veri yoksa
            var books = Books;

            // Cache'e ekleme
            await cacheService.SetAsync(cacheKey, books, TimeSpan.FromMinutes(10), cancellationToken);

            return Ok(ServiceResult<List<Book>>.Success(books, "Kitaplar başarıyla getirildi."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id, CancellationToken cancellationToken)
        {
            string cacheKey = $"book_{id}";

            // Cache kontrolü
            var cachedBook = await cacheService.GetAsync<Book>(cacheKey, cancellationToken);
            if (cachedBook != null)
            {
                return Ok(ServiceResult<Book>.Success(cachedBook, "Kitap önbellekten başarıyla getirildi."));
            }

            // Kitap bulunamazsa
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(ServiceResult<Book>.Fail(
                    "Bulunamadı",
                    $"{id} numaralı kitap mevcut değil",
                    StatusCodes.Status404NotFound
                ));
            }

            // Cache'e ekleme
            await cacheService.SetAsync(cacheKey, book, TimeSpan.FromMinutes(10), cancellationToken);

            return Ok(ServiceResult<Book>.Success(book, "Kitap başarıyla getirildi."));
        }


        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateDto requestBody, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(requestBody.Title))
            {
                return BadRequest(ServiceResult<Book>.Fail(
                    "Validasyon hatası",
                    "Kitap title ı girmek zorunludur",
                    StatusCodes.Status400BadRequest
                ));
            }

            var newBook = new Book
            {
                Id = Books.Count > 0 ? Books.Max(b => b.Id) + 1 : 1,
                Title = requestBody.Title,
                AuthorId = requestBody.AuthorId,
                CategoryId = requestBody.CategoryId,
            };

            Books.Add(newBook);

            // Önbelleği temizleme
            await cacheService.RemoveAsync("all_books", cancellationToken);

            return Ok(ServiceResult<Book>.Success(newBook, "Kitap başarıyla oluşturuldu."));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDto requestBody, CancellationToken cancellationToken)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound(ServiceResult<Book>.Fail(
                    "Bulunamadı",
                    $"{id} numaralı kitap bulunamadı",
                    StatusCodes.Status404NotFound
                ));
            }

            book.Title = requestBody.Title;
            book.AuthorId = requestBody.AuthorId;
            book.CategoryId = requestBody.CategoryId;

            // Cache güncelleme
            string cacheKey = $"book_{id}";
            await cacheService.SetAsync(cacheKey, book, TimeSpan.FromMinutes(10), cancellationToken);

            return Ok(ServiceResult<Book>.Success(book, "Kitap başarıyla güncellendi."));
        }


        [HttpPatch("{id}/stock")]
        public async Task<IActionResult> UpdateBookStock(int id, [FromBody] BookStockUpdateDto stockUpdateDto, CancellationToken cancellationToken)
        {
            // Redis cache key
            string cacheKey = $"book_{id}";

            // Kitap var mı kontrol et
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(ServiceResult<Book>.Fail(
                    "Bulunamadı",
                    $"{id} numaralı kitap bulunamadı",
                    StatusCodes.Status404NotFound
                ));
            }

            // StockQuantity güncelle
            book.StockQuantity = stockUpdateDto.StockQuantity;

            // Redis cache güncelle
            await cacheService.SetAsync(cacheKey, book, TimeSpan.FromMinutes(10), cancellationToken);

            // Ayrıca tüm kitapların önbelleğini temizle
            await cacheService.RemoveAsync("all_books", cancellationToken);

            return Ok(ServiceResult<Book>.Success(book, "Kitap stoğu başarıyla güncellendi."));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound(ServiceResult<Book>.Fail(
                    "Bulunamadı",
                    $"{id} numaralı kitap mevcut değil",
                    StatusCodes.Status404NotFound
                ));
            }

            Books.Remove(book);

            // Cache temizleme
            string cacheKey = $"book_{id}";
            await cacheService.RemoveAsync(cacheKey, cancellationToken);
            await cacheService.RemoveAsync("all_books", cancellationToken);

            return Ok(ServiceResult<Book>.Success(book, "Kitap başarıyla silindi."));
        }


        [HttpGet("paginated")]
        public async Task<IActionResult> GetBooksPaginated(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(ServiceResult<Book>.Fail(
                    "Geçersiz Parametreler",
                    "Page ve pageSize 0'dan büyük olması lazım",
                    StatusCodes.Status400BadRequest
                ));
            }

            string cacheKey = $"books_page_{page}_size_{pageSize}";

            // Cache kontrolü
            var cachedBooks = await cacheService.GetAsync<List<Book>>(cacheKey, cancellationToken);
            if (cachedBooks != null)
            {
                return Ok(ServiceResult<List<Book>>.Success(cachedBooks, "Kitaplar önbellekten başarıyla getirildi."));
            }

            var totalBooks = Books.Count;

            if (totalBooks == 0)
            {
                return NotFound(ServiceResult<Book>.Fail(
                    "Bulunamadı",
                    "Sayfalama için mevcut kitap bulunmamaktadır",
                    StatusCodes.Status404NotFound
                ));
            }

            var pagedBooks = Books
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Cache'e ekleme
            await cacheService.SetAsync(cacheKey, pagedBooks, TimeSpan.FromMinutes(10), cancellationToken);

            return Ok(ServiceResult<List<Book>>.Success(pagedBooks, "Kitaplar başarıyla getirildi."));
        }
    }
}
