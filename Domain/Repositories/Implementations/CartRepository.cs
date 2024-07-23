using e_commerce.Configurations;
using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace e_commerce.Domain.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<Cart> _carts;
        public CartRepository(IOptions<DatabaseSettings> options){

            var client = new MongoClient(options.Value.connectionString);
            var database = client.GetDatabase(options.Value.databaseName);
            this._carts = database.GetCollection<Cart>(options.Value.cartCollection);
        }
        public async Task<Cart> GetCart(string UserId){
            try
            {
                return await this._carts.Find(c => c.UserId == UserId).FirstOrDefaultAsync();
            }
            catch (FormatException){
                return null;
            }
        }
        public async Task update(Cart cart){
            await this._carts.ReplaceOneAsync(c=>c.Id == cart.Id, cart);
        }
        public async Task create(Cart cart){
           await this._carts.InsertOneAsync(cart);
        }
    }
}