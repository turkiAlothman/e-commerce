using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Configurations;
using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;

namespace e_commerce.Domain.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _UserCollection;
        public UserRepository(IOptions<DatabaseSettings> Settings){
           var client = new  MongoClient(Settings.Value.connectionString);
           var database = client.GetDatabase(Settings.Value.databaseName);
           this._UserCollection = database.GetCollection<User>(Settings.Value.userCollection); 

        }
         public async Task<User> getById(string id){
            return await this._UserCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
         }
        public  async Task<User> GetByEmail(Product Email){
            return await this._UserCollection.Find(u => u.Email.Equals(Email)).FirstOrDefaultAsync();
        } 
    }
}