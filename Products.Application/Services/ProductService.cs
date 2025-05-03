using Products.Application.Interfaces;
using Products.Domain.Entities;

namespace Products.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> GenerateUniqueProductId()
        {
            return await _repository.GenerateUniqueProductIdAsync();
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            product.ProductId = await GenerateUniqueProductId();
            return await _repository.AddProductAsync(product);
        }

        public async Task AddToStockAsync(string productId, int quantity)
        {
            var product = await _repository.GetProductById(productId);
            if (product == null)
            {
                throw new Exception("Product not avaialable");
            }
            else
            {
                product.StockAvailable += quantity;
                await _repository.UpdateProductAsync(product);
            }
        }

        public async Task DecrementStockAsync(string productId, int quantity)
        {
            var product = await _repository.GetProductById(productId);
            if (product == null)
            {
                throw new Exception("Product not avaialable");
            }
            else if (product.StockAvailable < quantity) throw new Exception("Insufficient stock");
            product.StockAvailable -= quantity;
            await _repository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(string productId)
        {
            var product = await _repository.GetProductById(productId);
            if (product == null)
            {
                throw new Exception("Product not avaialable");
            }
            else
            {
                await _repository.DeleteProductAsync(product);
            }

        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _repository.GetAllProductAsync();
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            return await _repository.GetProductById(productId);
        }

        public async Task UpdateProductAsync(string productId, Product product)
        {
            var existing = await _repository.GetProductById(productId);
            if (existing ==null) throw new Exception("Product not found");
            existing.ProductName = product.ProductName;
            existing.ProductDescription = product.ProductDescription;
            existing.StockAvailable = product.StockAvailable;
            await _repository.UpdateProductAsync(existing);
        }
    }
}
