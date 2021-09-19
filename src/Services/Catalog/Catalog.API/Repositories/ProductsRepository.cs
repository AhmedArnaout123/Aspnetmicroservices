using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductsRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _catalogContext
                            .Products
                            .Find(p => p.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string productName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, productName);

            return await _catalogContext
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);

            return await _catalogContext
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _catalogContext
                     .Products
                     .InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var updateResult = await _catalogContext
                     .Products
                     .ReplaceOneAsync(filter: filter, replacement: product);

            return updateResult.IsAcknowledged && (updateResult.ModifiedCount > 0);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await _catalogContext
                                        .Products
                                        .DeleteOneAsync(p => p.Id == id);

            return deleteResult.IsAcknowledged && (deleteResult.DeletedCount > 0);
        }
    }
}
