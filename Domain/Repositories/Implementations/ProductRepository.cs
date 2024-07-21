using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Configurations;
using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace e_commerce.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        
        public ProductRepository(IOptions<DatabaseSettings> options){

            var client = new MongoClient(options.Value.connectionString);
            var database = client.GetDatabase(options.Value.databaseName);
            this._products = database.GetCollection<Product>(options.Value.productCollection);
        }
        
        public async Task<List<Product>> all(){
            return await this._products.Find(_ => true).ToListAsync();
        }
        public async Task<Product> getById(string id){
            return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task create(Product product){
            await this._products.InsertOneAsync(product);
        }
        public async Task update(Product product){
            await this._products.ReplaceOneAsync(p => p.Id == product.Id,product);
        }

        public async Task delete(string id){
            await this._products.DeleteOneAsync(p => p.Id == id);
        }
        
    }
}