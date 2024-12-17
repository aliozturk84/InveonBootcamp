using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddBookSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "AvailableCopies", "Genre", "ISBN", "Language", "PageCount", "PublicationYear", "Publisher", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, "George Orwell", 5, "Dystopian", "9780451524935", "English", 328, 1949, "Secker & Warburg", "A dystopian novel about totalitarianism.", "1984" },
                    { 2, "Harper Lee", 7, "Fiction", "9780061120084", "English", 281, 1960, "J.B. Lippincott & Co.", "A novel about racial injustice in the Deep South.", "To Kill a Mockingbird" },
                    { 3, "F. Scott Fitzgerald", 4, "Classic", "9780743273565", "English", 180, 1925, "Charles Scribner's Sons", "A story about the American Dream in the Jazz Age.", "The Great Gatsby" },
                    { 4, "Herman Melville", 3, "Adventure", "9780142437247", "English", 585, 1851, "Harper & Brothers", "The saga of Captain Ahab and the white whale.", "Moby-Dick" },
                    { 5, "Jane Austen", 6, "Romance", "9780141439518", "English", 279, 1813, "T. Egerton", "A classic novel of manners and marriage.", "Pride and Prejudice" },
                    { 6, "J.D. Salinger", 8, "Fiction", "9780316769488", "English", 214, 1951, "Little, Brown and Company", "A story about teenage alienation and angst.", "The Catcher in the Rye" },
                    { 7, "J.R.R. Tolkien", 5, "Fantasy", "9780547928227", "English", 310, 1937, "George Allen & Unwin", "A tale of adventure with Bilbo Baggins.", "The Hobbit" },
                    { 8, "Fyodor Dostoevsky", 2, "Philosophical Fiction", "9780143058144", "English", 671, 1866, "The Russian Messenger", "A psychological drama of guilt and redemption.", "Crime and Punishment" },
                    { 9, "Paulo Coelho", 10, "Adventure", "9780061122415", "English", 208, 1988, "HarperTorch", "A philosophical story about pursuing dreams.", "The Alchemist" },
                    { 10, "Aldous Huxley", 4, "Dystopian", "9780060850524", "English", 268, 1932, "Chatto & Windus", "A dystopian story of a controlled society.", "Brave New World" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
