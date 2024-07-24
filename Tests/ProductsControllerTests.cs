using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductAPI.Controllers;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Tests
{
    public class ProductsControllerTests
    {
        private readonly ProductsController _controller;
        private readonly Mock<IProductService> _serviceMock;
        private readonly Mock<ILogger<ProductsController>> _loggerMock;

        public ProductsControllerTests()
        {
            _serviceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();
            _controller = new ProductsController(_serviceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void AddProduct_ShouldReturnOk_WhenProductIsValid()
        {
            var product = new Product
            {
                FirstName = "Charlie",
                LastName = "Brown",
                Description = "Good product",
                Quantity = 7
            };
            var result = _controller.AddProduct(product);
            var okResult = Assert.IsType<OkResult>(result);
            _serviceMock.Verify(s => s.AddProduct(product), Times.Once);
        }

        [Fact]
        public void AddProduct_ShouldReturnBadRequest_WhenProductIsInvalid()
        {
            _controller.ModelState.AddModelError("FirstName", "Required");
            var product = new Product
            {
                LastName = "Brown",
                Description = "Good product",
                Quantity = 7
            };
            var result = _controller.AddProduct(product);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    FirstName = "Daisy",
                    LastName = "Johnson",
                    Description = "Great product",
                    Quantity = 3
                }
            };
            _serviceMock.Setup(s => s.GetAllProducts()).Returns(products);
            var result = _controller.GetAllProducts();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnProducts = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Single(returnProducts);
        }

        [Fact]
        public void DeleteProduct_ShouldReturnOk_WhenProductIsDeleted()
        {
            _serviceMock.Setup(s => s.DeleteProduct(1)).Returns(true);
            var result = _controller.DeleteProduct(1);
            var okResult = Assert.IsType<OkResult>(result);
            _serviceMock.Verify(s => s.DeleteProduct(1), Times.Once);
        }

        [Fact]
        public void DeleteProduct_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            _serviceMock.Setup(s => s.DeleteProduct(999)).Returns(false);
            var result = _controller.DeleteProduct(999);
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}