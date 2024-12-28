using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess.Repositories.EntityFramework
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<User,Role,int>(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            DataSeederContributor.Seed(builder);

            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order - Course (One-to-Many)
            builder.Entity<Order>()
                .HasOne(o => o.Course)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment - Order (One-to-One)
            builder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            

        }
    }
}
