using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Products.Domain.Entities;

namespace Products.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> GetProductByIdAsync(string productId);
        Task<Product> AddProductAsync(Product product);
        Task UpdateProductAsync(string productId, Product product);
        Task DeleteProductAsync(string productId);
        Task DecrementStockAsync(string productId, int quantity);
        Task AddToStockAsync(string productId, int quantity);
    }
}
