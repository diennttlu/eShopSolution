using eShopSolution.Data.EntityFrameworkCores;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Catelog.Products;
using System.Collections.Generic;

namespace eShopSolution.Application.Catelog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EShopSolutionDbContext _dbContext;

        public PublicProductService(EShopSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var query = from p in _dbContext.Products
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _dbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            var data = await query
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
            return data;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryIdAsync(GetPublicProductPagingRequest request)
        {
            var query = from p in _dbContext.Products
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _dbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _dbContext.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };

            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
                query = query.Where(x => x.pic.CategoryId == request.CategoryId);

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
    }
}
