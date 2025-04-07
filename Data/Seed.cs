using imsapi.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace imsapi.Data
{
    public static class Seed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Store>().HasData(
            //     new Store { Id = 1, name = "Store 1" },
            //     new Store { Id = 2, name = "Store 2" }
            // );

            // modelBuilder.Entity<Admin>().HasData(
            //     new Admin { Id = 1, Phone = "1234567890" },
            //     new Admin { Id = 2, Phone = "0987654321" }
            // );

            // // modelBuilder.Entity<Category>().HasData(
            // //     new Category { Id = 1, storeId = 1, Name = "Category 1", Description = "Description 1" },
            // //     new Category { Id = 2, storeId = 2, Name = "Category 2", Description = "Description 2" }
            // // );

            // modelBuilder.Entity<Customer>().HasData(
            //     new Customer { Id = 1, storeId = 1, fullName = "Customer 1", info = "Info 1", phone = "1112223333", userId = 1 },
            //     new Customer { Id = 2, storeId = 2, fullName = "Customer 2", info = "Info 2", phone = "4445556666", userId = 2 }
            // );
            // modelBuilder.Entity<Payment>().HasData(
            //     new Payment { Id = 1, amount = 100.00m, paymentMethod = "Cash", storeId = 1, userId = 1, customerId = 1 },
            //     new Payment { Id = 2, amount = 200.00m, paymentMethod = "Credit Card", storeId = 2, userId = 2, customerId = 2 }
            // );
            
            
        }
    }
}