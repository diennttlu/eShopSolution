using eShopSolution.Application.Catelog.Products;
using eShopSolution.ViewModels.Catelog.ProductImages;
using eShopSolution.ViewModels.Catelog.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _productService;
        private readonly IManageProductService _manageProductService;

        public ProductController(IPublicProductService productService,
            IManageProductService manageProductService)
        {
            _productService = productService;
            _manageProductService = manageProductService;
        }

        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAsync(string languageId)
        {
            var products = await _productService.GetAllAsync(languageId);

            return Ok(products);
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetByIdAsync(int productId, string languageId)
        {
            var product = await _manageProductService.GetByIdAsync(productId, languageId);
            if (product == null)
            {
                return BadRequest($"Cannot find product with id: {productId}");
            }
            return Ok(product);
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageByIdAsync(int productId, int imageId)
        {
            var image = await _manageProductService.GetImageByIdAsync(imageId);
            if (image == null)
            {
                return BadRequest($"Cannot find image with id: {imageId}");
            }
            return Ok(image);
        }

        [HttpGet("public-paging")]
        public async Task<IActionResult> GetAsync([FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _productService.GetAllByCategoryIdAsync(request);

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductService.CreateAsync(request);
            if (productId == 0)
            {
                return BadRequest();
            }
            var product = await _manageProductService.GetByIdAsync(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = productId }, product);
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateAsync(int productId, [FromBody] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageProductService.AddImageAsync(productId, request);
            if (imageId == 0)
            {
                return BadRequest();
            }
            var productImage = await _manageProductService.GetImageByIdAsync(imageId);
            return CreatedAtAction(nameof(GetImageByIdAsync), new { id = imageId }, productImage);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateAsync(int productId, int imageId, [FromBody] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageProductService.UpdateImageAsync(imageId, request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageProductService.UpdateAsync(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPatch("price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePriceAsync([FromQuery] int productId, decimal newPrice)
        {
            var isSuccessfull = await _manageProductService.UpdatePriceAsync(productId, newPrice);
            if (!isSuccessfull)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPatch("stock/{productId}/{quantity}")]
        public async Task<IActionResult> UpdateStockAsync([FromQuery] int productId, int quantity)
        {
            var isSuccessfull = await _manageProductService.UpdateStockAsync(productId, quantity);
            if (!isSuccessfull)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPatch("view-count/{productId}")]
        public async Task<IActionResult> UpdateViewCountAsync(int productId)
        {
            await _manageProductService.UpdateViewCountAsync(productId);

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            var deletedResult = await _manageProductService.DeleteAsync(productId);
            if (deletedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteAsync(int productId, int imageId)
        {
            var deletedResult = await _manageProductService.RemoveImageAsync(imageId);
            if (deletedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}