using eShopSolution.Data.Entities;
using eShopSolution.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.EF
{
    public static class EShopSolutionModelBuilderExtensions 
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Áo Nam", IsShowOnHome = true, SeoAlias = "ao-nam", SeoDescription = "Sản phẩm thời trang nam", SeoTitle = "Sản phẩm thời trang nam" },
                new Category() { Id = 2, Name = "Áo Nữ", IsShowOnHome = true, SeoAlias = "ao-nu", SeoDescription = "Sản phẩm thời trang nữ", SeoTitle = "Sản phẩm thời trang nữ" },
                new Category() { Id = 3, Name = "Giày Nam", IsShowOnHome = true, SeoAlias = "giay-nam", SeoDescription = "Giày danh cho nam", SeoTitle = "Giày danh cho nam" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Áo phông nam cổ tim", Price = 80000, OriginalPrice = 50000, Stock = 10, ViewCount = 0, Status = Status.Active },
                new Product() { Id = 2, Name = "Giày Ultraboot 4.0", Price = 500000, OriginalPrice = 390000, Stock = 10, ViewCount = 0, Status = Status.Active }
                );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory() 
                { 
                    CategoryId = 1, 
                    ProductId = 1
                });

            var roleId = new Guid("B7C261E0-6A2D-4DA7-83B5-35AA3E4F53B6");
            var userId = new Guid("8066F6B9-B3E4-473B-AEC3-DF30285FD486");

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = userId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@gamil.com",
                NormalizedEmail = "admin@gamil.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Tu Dien",
                LastName = "Nguyen",
                Dob = new DateTime(2020,01,30)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = userId
            });
        }
    }
}
