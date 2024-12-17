using LibraryManagementApp.Web.Models.Entities;

namespace LibraryManagementApp.Web.Models.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> SearchBooksAsync(string searchText);
    }

}
