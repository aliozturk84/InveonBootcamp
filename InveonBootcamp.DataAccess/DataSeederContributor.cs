using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess
{
    public static class DataSeederContributor
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedCourses(modelBuilder);
            SeedOrders(modelBuilder);
            SeedPayments(modelBuilder);
        }

        private static void SeedCourses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Name = "Introduction to Programming",
                    Description = "Learn the basics of programming using C#.",
                    Price = 199.99m,
                    Category = "Programming"
                },
                new Course
                {
                    Id = 2,
                    Name = "Advanced Java",
                    Description = "Deep dive into Java for experienced developers.",
                    Price = 299.99m,
                    Category = "Programming"
                },
                new Course
                {
                    Id = 3,
                    Name = "Web Development with ASP.NET",
                    Description = "Build dynamic websites using ASP.NET Core.",
                    Price = 249.99m,
                    Category = "Web Development"
                }
            );
        }

        private static void SeedOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    UserId = 1, // User1
                    CourseId = 1 // Course1
                },
                new Order
                {
                    Id = 2,
                    UserId = 2, // User2
                    CourseId = 2 // Course2
                },
                new Order
                {
                    Id = 3,
                    UserId = 3, // User3
                    CourseId = 3 // Course3
                }
            );
        }

        private static void SeedPayments(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    OrderId = 1, // Order1
                    Amount = 199.99m,
                    PaymentDate = DateTime.UtcNow
                },
                new Payment
                {
                    Id = 2,
                    OrderId = 2, // Order2
                    Amount = 299.99m,
                    PaymentDate = DateTime.UtcNow
                },
                new Payment
                {
                    Id = 3,
                    OrderId = 3, // Order3
                    Amount = 249.99m,
                    PaymentDate = DateTime.UtcNow
                }
            );
        }
    }
}
