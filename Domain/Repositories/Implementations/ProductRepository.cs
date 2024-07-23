using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Configurations;
using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
using e_commerce.DTOs.Responses;
using e_commerce.utils;
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
        
        public async Task<List<Product>> all(int pageNumber, int pageSize){
            int start = (pageNumber -1) * pageSize;
            
            return await this._products.Find(_ => true).Skip(start).Limit(pageSize).ToListAsync();
        }
        public async Task<Product> getById(string id){
             try
            {
                return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
            }
             catch (FormatException e)
            {
                return null;
            }
            
        }
        public async Task create(Product product){
            await this._products.InsertOneAsync(product);
        }
        public async Task update(Product product){
            await this._products.ReplaceOneAsync(p => p.Id == product.Id,product);
        }

        public async Task updateRange(IEnumerable<Product> products){
            var bluik =  new List<WriteModel<Product>>();
            foreach (var product in products)
            {
                var filter = Builders<Product>.Filter.Eq(p=> p.Id,product.Id);
                var update =  Builders<Product>.Update.Set(p => p.Quantity, product.Quantity);
                var updateModel = new UpdateOneModel<Product>(filter, update);
                bluik.Add(updateModel);
            }

            if(bluik.Any()){
                await this._products.BulkWriteAsync(bluik);
            }
        }

        public async Task delete(string id){
            await this._products.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<long> count(){
            return await this._products.CountDocumentsAsync(_=> true);
        }
        
        public async Task<IEnumerable<ShoppingCartItem>> join(List<ProductItem> ids){
            
            var Filter =  Builders<Product>.Filter.In(p=>p.Id, ids.Select(p=>p.ProductId));
            List<Product> products =  this._products.Find<Product>(Filter).ToList();
            
            var joinList = from productItem in ids
            join product in products on productItem.ProductId equals product.Id
            select new ShoppingCartItem {product = product , count = productItem.Quentity};
            return joinList;
        }
    }
}