using Microsoft.EntityFrameworkCore;
using LibraryManagementApp.Web.Models.Entities;

public static class BookSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "1984",
                Author = "George Orwell",
                PublicationYear = 1949,
                ISBN = "9780451524935",
                Genre = "Dystopian",
                Publisher = "Secker & Warburg",
                PageCount = 328,
                Language = "English",
                Summary = "A dystopian novel about totalitarianism.",
                AvailableCopies = 5
            },
            new Book
            {
                Id = 2,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                PublicationYear = 1960,
                ISBN = "9780061120084",
                Genre = "Fiction",
                Publisher = "J.B. Lippincott & Co.",
                PageCount = 281,
                Language = "English",
                Summary = "A novel about racial injustice in the Deep South.",
                AvailableCopies = 7
            },
            new Book
            {
                Id = 3,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                PublicationYear = 1925,
                ISBN = "9780743273565",
                Genre = "Classic",
                Publisher = "Charles Scribner's Sons",
                PageCount = 180,
                Language = "English",
                Summary = "A story about the American Dream in the Jazz Age.",
                AvailableCopies = 4
            },
            new Book
            {
                Id = 4,
                Title = "Moby-Dick",
                Author = "Herman Melville",
                PublicationYear = 1851,
                ISBN = "9780142437247",
                Genre = "Adventure",
                Publisher = "Harper & Brothers",
                PageCount = 585,
                Language = "English",
                Summary = "The saga of Captain Ahab and the white whale.",
                AvailableCopies = 3
            },
            new Book
            {
                Id = 5,
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                PublicationYear = 1813,
                ISBN = "9780141439518",
                Genre = "Romance",
                Publisher = "T. Egerton",
                PageCount = 279,
                Language = "English",
                Summary = "A classic novel of manners and marriage.",
                AvailableCopies = 6
            },
            new Book
            {
                Id = 6,
                Title = "The Catcher in the Rye",
                Author = "J.D. Salinger",
                PublicationYear = 1951,
                ISBN = "9780316769488",
                Genre = "Fiction",
                Publisher = "Little, Brown and Company",
                PageCount = 214,
                Language = "English",
                Summary = "A story about teenage alienation and angst.",
                AvailableCopies = 8
            },
            new Book
            {
                Id = 7,
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                PublicationYear = 1937,
                ISBN = "9780547928227",
                Genre = "Fantasy",
                Publisher = "George Allen & Unwin",
                PageCount = 310,
                Language = "English",
                Summary = "A tale of adventure with Bilbo Baggins.",
                AvailableCopies = 5
            },
            new Book
            {
                Id = 8,
                Title = "Crime and Punishment",
                Author = "Fyodor Dostoevsky",
                PublicationYear = 1866,
                ISBN = "9780143058144",
                Genre = "Philosophical Fiction",
                Publisher = "The Russian Messenger",
                PageCount = 671,
                Language = "English",
                Summary = "A psychological drama of guilt and redemption.",
                AvailableCopies = 2
            },
            new Book
            {
                Id = 9,
                Title = "The Alchemist",
                Author = "Paulo Coelho",
                PublicationYear = 1988,
                ISBN = "9780061122415",
                Genre = "Adventure",
                Publisher = "HarperTorch",
                PageCount = 208,
                Language = "English",
                Summary = "A philosophical story about pursuing dreams.",
                AvailableCopies = 10
            },
            new Book
            {
                Id = 10,
                Title = "Brave New World",
                Author = "Aldous Huxley",
                PublicationYear = 1932,
                ISBN = "9780060850524",
                Genre = "Dystopian",
                Publisher = "Chatto & Windus",
                PageCount = 268,
                Language = "English",
                Summary = "A dystopian story of a controlled society.",
                AvailableCopies = 4
            }
        );
    }
}
