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
            _nextId = 1; // Start IDs from 1
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
            _deletedIds.Add(id); // Add ID to the deleted IDs
            return true;
        }

        private bool ValidateProduct(Product product)
        {
            // Check if LastName and Description are not empty and Quantity is within the valid range
            if (string.IsNullOrWhiteSpace(product.LastName) ||
                string.IsNullOrWhiteSpace(product.Description) ||
                product.Quantity < 1 ||
                product.Quantity > 20 ||
                product.FirstName.Length > 20 ||  // Check FirstName length
                product.LastName.Length > 20 ||   // Check LastName length
                product.Description.Length > 100) // Check Description length
            {
                return false;
            }
            return true;
        }
    }
}