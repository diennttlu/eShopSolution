using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.EntityFrameworkCores
{
    public static class EShopSolutionModelCreatingExtension
    {
        public static void ConfigureEShopSolution(this ModelBuilder builder)
        {
            builder.Entity<AppConfig>(e =>
            {
                e.ToTable("AppConfigs");

                e.HasKey(x => x.Key);

                e.Property(x => x.Value).IsRequired(true);
            });

            builder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).UseIdentityColumn();

                e.Property(x => x.Price).IsRequired();
                e.Property(x => x.OriginalPrice).IsRequired();
                e.Property(x => x.Stock).IsRequired().HasDefaultValue(0);
                e.Property(x => x.ViewCount).IsRequired().HasDefaultValue(0);
            });

            builder.Entity<Cart>(e =>
            {
                e.ToTable("Carts");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).UseIdentityColumn();

                e.HasOne(x => x.Product).WithMany(x => x.Carts).HasForeignKey(x => x.ProductId);
            });

            builder.Entity<Category>(e =>
            {
                e.ToTable("Categories");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).UseIdentityColumn();

                e.Property(x => x.Status).HasDefaultValue(Status.Active);
            });

            builder.Entity<CategoryTranslation>(e =>
            {
                e.ToTable("CategoryTranslations");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).UseIdentityColumn();

                e.Property(x => x.Name).IsRequired().HasMaxLength(200);
                
                e.Property(x => x.SeoAlias).IsRequired().HasMaxLength(200);
                
                e.Property(x => x.SeoDescription).HasMaxLength(500);
                
                e.Property(x => x.SeoTitle).HasMaxLength(200);
                
                e.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);
                
                e.HasOne(x => x.Language).WithMany(x => x.CategoryTranslations).HasForeignKey(x => x.LanguageId);
                
                e.HasOne(x => x.Category).WithMany(x => x.CategoryTranslations).HasForeignKey(x => x.CategoryId);
            });

            builder.Entity<Contact>(e =>
            {
                e.ToTable("Contacts");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).UseIdentityColumn();
                
                e.Property(x => x.Name).HasMaxLength(200).IsRequired();
                
                e.Property(x => x.Email).HasMaxLength(200).IsRequired();

                e.Property(x => x.PhoneNumber).HasMaxLength(200).IsRequired();

                e.Property(x => x.Message).IsRequired();
            });

            builder.Entity<Language>(e =>
            {
                e.ToTable("Languages");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).IsRequired().IsUnicode(false).HasMaxLength(5);
                
                e.Property(x => x.Name).IsRequired().HasMaxLength(20);
            });

            builder.Entity<Order>(e =>
            {
                e.ToTable("Orders");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).UseIdentityColumn();
                
                e.Property(x => x.OrderDate).HasDefaultValue(DateTime.Now);
                
                e.Property(x => x.ShipEmail).IsRequired().IsUnicode(false).HasMaxLength(50);
                
                e.Property(x => x.ShipAddress).IsRequired().HasMaxLength(200);

                e.Property(x => x.ShipName).IsRequired().HasMaxLength(200);

                e.Property(x => x.ShipPhoneNumber).IsRequired().HasMaxLength(200);
            });

            builder.Entity<OrderDetail>(e =>
            {
                e.ToTable("OrderDetails");
                e.HasKey(x => new { x.OrderId, x.ProductId });

                e.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);
                e.HasOne(x => x.Product).WithMany(x => x.OrderDetails).HasForeignKey(x => x.ProductId);
            });

            builder.Entity<ProductInCategory>(e =>
            {
                e.HasKey(x => new { x.CategoryId, x.ProductId });

                e.ToTable("ProductInCategories");

                e.HasOne(x => x.Product).WithMany(pc => pc.ProductInCategories)
                    .HasForeignKey(pc => pc.ProductId);

                e.HasOne(x => x.Category).WithMany(pc => pc.ProductInCategories)
                  .HasForeignKey(pc => pc.CategoryId);
            });

            builder.Entity<ProductTranslation>(e =>
            {
                e.ToTable("ProductTranslations");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).UseIdentityColumn();
                
                e.Property(x => x.Name).IsRequired().HasMaxLength(200);
                
                e.Property(x => x.SeoAlias).IsRequired().HasMaxLength(200);

                e.Property(x => x.Details).HasMaxLength(500);

                e.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);
                
                e.HasOne(x => x.Language).WithMany(x => x.ProductTranslations).HasForeignKey(x => x.LanguageId);
                
                e.HasOne(x => x.Product).WithMany(x => x.ProductTranslations).HasForeignKey(x => x.ProductId);
            });

            builder.Entity<Promotion>(e =>
            {
                e.ToTable("Promotions");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).UseIdentityColumn();

                e.Property(x => x.Name).IsRequired();
            });

            builder.Entity<Transaction>(e =>
            {
                e.ToTable("Transactions");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).UseIdentityColumn();
            });
        }
    }
}
