namespace LibraryManagementApp.Web.Models.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        Task<int> CompleteAsync();
    }
}
