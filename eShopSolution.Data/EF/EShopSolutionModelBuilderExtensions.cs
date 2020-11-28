using eShopSolution.Data.Entities;
using eShopSolution.Shared.Enums;
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
        }
    }
}
