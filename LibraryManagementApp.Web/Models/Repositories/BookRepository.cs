using LibraryManagementApp.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Web.Models.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchText)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(searchText) ||
                            b.Author.Contains(searchText) ||
                            b.ISBN.Contains(searchText))
                .ToListAsync();
        }
    }
}
