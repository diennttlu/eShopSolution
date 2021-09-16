using eShopSolution.ViewModels.Catelog.ProductImages;
using eShopSolution.ViewModels.Catelog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catelog.Products
{
    public interface IManageProductService
    {
        Task<int> CreateAsync(ProductCreateRequest request);

        Task<int> UpdateAsync(ProductUpdateRequest request);

        Task<bool> UpdatePriceAsync(int productId, decimal price);

        Task<bool> UpdateStockAsync(int productId, int quantity);

        Task UpdateViewCountAsync(int productId);

        Task<int> DeleteAsync(int productId);

        Task<PagedResult<ProductViewModel>> GetAllPagingAsync(GetManageProductPagingRequest request);

        Task<ProductViewModel> GetByIdAsync(int productId, string languageId);

        Task<int> AddImageAsync(int productId, ProductImageCreateRequest request);

        Task<int> RemoveImageAsync(int imageId);

        Task<int> UpdateImageAsync(int imageId, ProductImageUpdateRequest request);

        Task<ProductImageViewModel> GetImageByIdAsync(int imageId);

        Task<List<ProductImageViewModel>> GetImages(int productId);
    }
}