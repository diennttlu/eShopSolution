using eShopSolution.Data.Entities;
using eShopSolution.Data.EntityFrameworkCores;
using eShopSolution.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Catelog.Products;
using Microsoft.AspNetCore.Http;
using System.IO;
using eShopSolution.Application.Common;
using eShopSolution.ViewModels.Catelog.ProductImages;

namespace eShopSolution.Application.Catelog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopSolutionDbContext _dbContext;
        private readonly IStorageService _storageService;

        public ManageProductService(EShopSolutionDbContext dbContext, IStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }

        public async Task<int> CreateAsync(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };

            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> UpdateAsync(ProductUpdateRequest request)
        {
            var query = _dbContext.Products.Include(x => x.ProductTranslations).AsQueryable();
            var product = await query.FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                x.ProductTranslations.Any(x => x.LanguageId == request.LanguageId));

            if (product == null)
                throw new EShopSolutionException($"Cannot find a product with id: {request.Id}");

            var productTranslation = product.ProductTranslations
                .FirstOrDefault(x => x.LanguageId == request.LanguageId);

            productTranslation.Name = request.Name;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.Description = request.Description;
            productTranslation.Details = request.Details;

            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _dbContext.ProductImages
                    .FirstOrDefaultAsync(x => x.IsDefault && x.ProductId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _dbContext.ProductImages.Update(thumbnailImage);
                }
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePriceAsync(int productId, decimal price)
        {
            var product = await _dbContext.Products.FindAsync(productId);

            if (product == null)
                throw new EShopSolutionException($"Cannot find a product with id: {productId}");
            product.Price = price;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantity)
        {
            var product = await _dbContext.Products.FindAsync(productId);

            if (product == null)
                throw new EShopSolutionException($"Cannot find a product with id: {productId}");
            product.Stock += quantity;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task UpdateViewCountAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int productId)
        {
            var product = await _dbContext.Products
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null)
            {
                throw new EShopSolutionException($"Cannot find a product: {productId}");
            }
            foreach (var image in product.ProductImages)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }
            _dbContext.Products.Remove(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPagingAsync(GetManageProductPagingRequest request)
        {
            var query = from p in _dbContext.Products
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _dbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == request.LanguageId
                        select new { p, pt, pic };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

            if (request.CategoryIds.Count > 0)
                query = query.Where(x => request.CategoryIds.Contains(x.pic.CategoryId));

            var totalRow = await query.CountAsync();
            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                })
                .ToListAsync();

            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };

            return pageResult;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = System.Net.Http.Headers.ContentDispositionHeaderValue
                .Parse(file.ContentDisposition)
                .FileName
                .Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ProductViewModel> GetByIdAsync(int productId, string languageId)
        {
            var query = from p in _dbContext.Products
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        where p.Id == productId && pt.LanguageId == languageId
                        select new { p, pt };

            var data = await query.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.pt.Name,
                DateCreated = x.p.DateCreated,
                Description = x.pt.Description,
                Details = x.pt.Details,
                LanguageId = x.pt.LanguageId,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                SeoAlias = x.pt.SeoAlias,
                SeoDescription = x.pt.SeoDescription,
                SeoTitle = x.pt.SeoTitle,
                Stock = x.p.Stock,
                ViewCount = x.p.ViewCount
            }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<int> AddImageAsync(int productId, ProductImageCreateRequest request)
        {
            var image = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder,
            };

            if (request.ImageFile != null)
            {
                image.ImagePath = await SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;
            }
            _dbContext.ProductImages.Add(image);
            await _dbContext.SaveChangesAsync();
            return image.Id;
        }

        public async Task<int> RemoveImageAsync(int imageId)
        {
            var productImage = await _dbContext.ProductImages.FindAsync(imageId);
            if (productImage == null)
            {
                throw new EShopSolutionException($"Cannot find an image with id: {imageId}");
            }
            _dbContext.ProductImages.Remove(productImage);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateImageAsync(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _dbContext.ProductImages.FindAsync(imageId);
            if (productImage == null)
            {
                throw new EShopSolutionException($"Cannot find an image with id: {imageId}");
            }
            productImage.Caption = request.Caption;
            productImage.IsDefault = request.IsDefault;
            productImage.SortOrder = request.SortOrder;

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _dbContext.ProductImages.Update(productImage);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetImages(int productId)
        {
            return await _dbContext.ProductImages.Where(x => x.ProductId == productId)
                .Select(x => new ProductImageViewModel
                {
                    Id = x.Id,
                    Caption = x.Caption,
                    IsDefault = x.IsDefault,
                    ImagePath = x.ImagePath,
                    SortOrder = x.SortOrder,
                    FileSize = x.FileSize,
                    DateCreated = x.DateCreated,
                    ProductId = x.ProductId
                }).ToListAsync();
        }

        public async Task<ProductImageViewModel> GetImageByIdAsync(int imageId)
        {
            var productImage = await _dbContext.ProductImages.FindAsync(imageId);
            if (productImage == null)
            {
                throw new EShopSolutionException($"Cannot find an image with id: {imageId}");
            }
            return new ProductImageViewModel()
            {
                Id = productImage.Id,
                Caption = productImage.Caption,
                IsDefault = productImage.IsDefault,
                ImagePath = productImage.ImagePath,
                SortOrder = productImage.SortOrder,
                FileSize = productImage.FileSize,
                DateCreated = productImage.DateCreated,
                ProductId = productImage.ProductId
            };
        }
    }
}