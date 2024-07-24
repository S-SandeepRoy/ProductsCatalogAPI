using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _service = new ProductService();
        }

        [Fact]
        public void AddProduct_ShouldAddProductWithValidData()
        {
            var product = new Product
            {
                FirstName = "Alice",
                LastName = "Smith",
                Description = "High-quality product",
                Quantity = 5
            };
            _service.AddProduct(product);
            var addedProduct = _service.GetAllProducts().FirstOrDefault();
            Assert.NotNull(addedProduct);
            Assert.Equal("Alice", addedProduct.FirstName);
            Assert.Equal("Smith", addedProduct.LastName);
            Assert.Equal("High-quality product", addedProduct.Description);
            Assert.Equal(5, addedProduct.Quantity);
        }

        [Fact]
        public void AddProduct_ShouldNotAddProductWithInvalidData()
        {
            var product = new Product
            {
                FirstName = new string('a', 21),
                LastName = "Smith",
                Description = "High-quality product",
                Quantity = 5
            };
            _service.AddProduct(product);
            var addedProduct = _service.GetAllProducts().FirstOrDefault(p => p.FirstName == new string('a', 21));
            Assert.Null(addedProduct);
        }

        [Fact]
        public void DeleteProduct_ShouldRemoveProductSuccessfully()
        {
            var product = new Product
            {
                FirstName = "Bob",
                LastName = "Jones",
                Description = "Sample product",
                Quantity = 10
            };
            _service.AddProduct(product);
            var addedProduct = _service.GetAllProducts().FirstOrDefault();
            int productId = addedProduct.Id;
            var result = _service.DeleteProduct(productId);
            Assert.True(result);
            var deletedProduct = _service.GetAllProducts().FirstOrDefault(p => p.Id == productId);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public void DeleteProduct_ShouldReturnFalseIfProductNotFound()
        {
            var result = _service.DeleteProduct(999);
            Assert.False(result);
        }
    }
}