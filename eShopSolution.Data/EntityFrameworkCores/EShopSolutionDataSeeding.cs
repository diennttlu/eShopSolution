using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.EntityFrameworkCores
{
    public static class EShopSolutionDataSeeding
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<AppConfig>(e =>
            {
                e.HasData(
                    new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
                    new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of eShopSolution" },
                    new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopSolution" });
            });

            builder.Entity<Language>(e =>
            {
                e.HasData(
                    new Language() { Id = "vi-VN", Name = "Tiếng Việt", IsDefault = true },
                    new Language() { Id = "en-US", Name = "English", IsDefault = false }
                );
            });

            builder.Entity<Category>(e =>
            {
                e.HasData(
                    new Category()
                    {
                        Id = 1,
                        IsShowOnHome = true,
                        ParentId = null,
                        SortOrder = 1,
                        Status = Status.Active
                    });
            });

            builder.Entity<CategoryTranslation>(e =>
            {
                e.HasData(
                     new CategoryTranslation()
                     {
                         Id = 1,
                         Name = "Áo nam",
                         LanguageId = "vi-VN",
                         SeoAlias = "ao-nam",
                         SeoTitle = "Sản phẩm áo thời trang nam",
                         SeoDescription = "Sản phẩm áo thời trang nam",
                         CategoryId = 1
                     },
                     new CategoryTranslation()
                     {
                         Id = 2,
                         Name = "Men shirt",
                         LanguageId = "en-US",
                         SeoAlias = "men-shirt",
                         SeoTitle = "The shirt products for men",
                         SeoDescription = "The shirt products for men",
                         CategoryId = 1
                     });
            });

            builder.Entity<Product>(e =>
            {
                e.HasData(
                    new Product()
                    {
                        Id = 1,
                        DateCreated = DateTime.Now,
                        OriginalPrice = 100000,
                        Price = 200000,
                        Stock = 0,
                        ViewCount = 0,
                    });
            });

            builder.Entity<ProductTranslation>(e =>
            {
                e.HasData(
                    new ProductTranslation()
                    {
                        Id = 1,
                        Name = "Áo sơ mi nam trắng Việt Tiến",
                        LanguageId = "vi-VN",
                        SeoAlias = "ao-so-mi-nam-trang-viet-tien",
                        SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
                        SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                        Details = "Mô tả sản phẩm",
                        ProductId = 1
                    },
                    new ProductTranslation()
                    {
                        Id = 2,
                        Name = "Viet Tien Men T-shirt",
                        LanguageId = "en-US",
                        SeoAlias = "viet-tien-men-t-shirt",
                        SeoTitle = "Viet Tien Men T-shirt",
                        SeoDescription = "Viet Tien Men T-shirt",
                        Details = "Description for product",
                        ProductId = 1
                    });
            });

            builder.Entity<ProductInCategory>(e => 
            {
                e.HasData(new ProductInCategory() { CategoryId = 1, ProductId = 1 });
            });

            var roleId = new Guid("0FB72392-62D8-41CE-B809-F78BD9DE2E82");
            var adminId = new Guid("6A3C2B02-7427-4FFE-8DF7-A635A5A58758");

            builder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "ADMIN",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();

            builder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "diennttlu@gmail.com",
                NormalizedEmail = "diennttlu@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abc1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Tu",
                LastName = "Dien",
                DateOfBirth = new DateTime(2020, 1, 31)
            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>()
            {
                RoleId = roleId,
                UserId = adminId
            });

        }
    }
}
