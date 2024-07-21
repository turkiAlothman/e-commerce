using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Domain.Models;

namespace e_commerce.Domain.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> all();
        public Task<Product> getById(string id);
        public Task create(Product product);
        public Task update(Product product);
        public Task delete(string id);
    }
}