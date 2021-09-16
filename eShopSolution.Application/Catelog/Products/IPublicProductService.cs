using eShopSolution.ViewModels.Catelog.Products;
using eShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catelog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryIdAsync(GetPublicProductPagingRequest request);

        Task<List<ProductViewModel>> GetAllAsync(string languageId);
    }
}
