using LibraryManagementApp.Web.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LibraryManagementApp.Web.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<AppUser, AppRole, Guid>(options)
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            BookSeeder.Seed(builder);

            builder.Entity<UserFeature>().HasKey(x => x.UserId);

            builder.Entity<UserFeature>().HasOne(x => x.AppUser)
                .WithOne(x => x.UserFeature)
                .HasForeignKey<UserFeature>(x => x.UserId);
        }
    }
}
