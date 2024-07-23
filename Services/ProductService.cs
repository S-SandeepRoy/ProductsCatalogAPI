using ProductAPI.Models;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;
        private readonly HashSet<int> _deletedIds;
        private int _nextId;

        public ProductService()
        {
            _products = new List<Product>();
            _deletedIds = new HashSet<int>();
            _nextId = 1;
        }

        public void AddProduct(Product product)
        {
            if (!ValidateProduct(product)) return;

            product.Id = _deletedIds.Count > 0 ? _deletedIds.First() : _nextId++;
            if (_deletedIds.Contains(product.Id))
            {
                _deletedIds.Remove(product.Id);
            }
            _products.Add(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public bool DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return false;

            _products.Remove(product);
            _deletedIds.Add(id);
            return true;
        }

        private bool ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.LastName) ||
                string.IsNullOrWhiteSpace(product.Description) ||
                product.Quantity < 1 ||
                product.Quantity > 20 ||
                product.FirstName.Length > 20 ||
                product.LastName.Length > 20 ||
                product.Description.Length > 100)
            {
                return false;
            }
            return true;
        }
    }
}