using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Products.Application.Interfaces;
using Products.Domain.Entities;
using Products.Infrastructure.Data;

namespace Products.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext _context;
        public ProductRepository(ProductsDbContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateUniqueProductIdAsync()
        {
            await _context.Database.OpenConnectionAsync();
            try
            {
                using(var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT NEXT VALUE FOR ProductIdSequence";
                    var result = await command.ExecuteScalarAsync();
                    int id = Convert.ToInt32(result);
                    return id.ToString("D6");
                }
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
            
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
