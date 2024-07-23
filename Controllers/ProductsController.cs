using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> Log;

        public ProductsController(IProductService productService, ILogger<ProductsController> _logger)
        {
            Log = _logger;
            _productService = productService;
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                Log.LogError($"Model State is invalid: {ModelState}");
                return BadRequest(ModelState);
            }

            _productService.AddProduct(product);
            Log.LogInformation("Added product successfully");
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            Log.LogInformation("Extracted all products successfully");
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var result = _productService.DeleteProduct(id);
            if (!result)
            {
                Log.LogWarning($"Product not found");
                return NotFound();
            }
            Log.LogInformation($"Deleted product with id '{id}' successfully");
            return Ok();
        }
    }
}