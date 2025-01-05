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
                    Category = "Programming",
                    InstructorId = 3,
                },
                new Course
                {
                    Id = 2,
                    Name = "Advanced Java",
                    Description = "Deep dive into Java for experienced developers.",
                    Price = 299.99m,
                    Category = "Programming",
                    InstructorId = 3,
                },
                new Course
                {
                    Id = 3,
                    Name = "Python for Beginners",
                    Description = "Master the fundamentals of Python programming.",
                    Price = 149.99m,
                    Category = "Programming",
                    InstructorId = 3,
                },

                // Web Development
                new Course
                {
                    Id = 4,
                    Name = "Web Development with ASP.NET",
                    Description = "Build dynamic websites using ASP.NET Core.",
                    Price = 249.99m,
                    Category = "Web Development",
                    InstructorId = 3,
                },
                new Course
                {
                    Id = 5,
                    Name = "React for Beginners",
                    Description = "Learn the basics of React.js for front-end development.",
                    Price = 199.99m,
                    Category = "Web Development",
                    InstructorId = 3,
                },
                new Course
                {
                    Id = 6,
                    Name = "Full-Stack Development",
                    Description = "Master front-end and back-end development skills.",
                    Price = 399.99m,
                    Category = "Web Development",
                    InstructorId = 3,
                },

                // Data Science
                new Course
                {
                    Id = 7,
                    Name = "Introduction to Data Science",
                    Description = "Explore the world of data science and machine learning.",
                    Price = 299.99m,
                    Category = "Data Science",
                    InstructorId = 3,
                },
                new Course
                {
                    Id = 8,
                    Name = "Data Visualization with Python",
                    Description = "Learn how to create stunning data visualizations using Python.",
                    Price = 249.99m,
                    Category = "Data Science",
                    InstructorId = 3,
                },
                new Course
                {
                    Id = 9,
                    Name = "Machine Learning Basics",
                    Description = "Dive into the foundations of machine learning.",
                    Price = 349.99m,
                    Category = "Data Science",
                    InstructorId = 3,
                },

                // Cloud Computing
                new Course
                {
                    Id = 10,
                    Name = "Introduction to AWS",
                    Description = "Get started with Amazon Web Services for cloud computing.",
                    Price = 299.99m,
                    Category = "Cloud Computing",
                    InstructorId = 3,
                },
                new Course
                {
                    Id = 11,
                    Name = "Microsoft Azure Essentials",
                    Description = "Learn the basics of Microsoft Azure cloud platform.",
                    Price = 279.99m,
                    Category = "Cloud Computing",
                    InstructorId = 3,
                },
                new Course
                {
                    Id = 12,
                    Name = "Google Cloud Platform Fundamentals",
                    Description = "Understand the fundamentals of Google Cloud Platform.",
                    Price = 259.99m,
                    Category = "Cloud Computing",
                    InstructorId = 3,
                },

                // Cybersecurity
                new Course
                {
                    Id = 13,
                    Name = "Cybersecurity Basics",
                    Description = "Learn the fundamentals of securing digital systems.",
                    Price = 199.99m,
                    Category = "Cybersecurity",
                    InstructorId = 3,
                },
                new Course
                {
                    Id = 14,
                    Name = "Ethical Hacking",
                    Description = "Understand ethical hacking principles and techniques.",
                    Price = 349.99m,
                    Category = "Cybersecurity",
                    InstructorId = 3,
                },
                new Course
                {
                    Id = 15,
                    Name = "Network Security",
                    Description = "Master the concepts of securing network infrastructure.",
                    Price = 299.99m,
                    Category = "Cybersecurity",
                    InstructorId = 3,
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