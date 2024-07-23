using ProductAPI.Models;

namespace ProductAPI.Services
{
    public interface IProductService
    {
        void AddProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        bool DeleteProduct(int id);
    }
}