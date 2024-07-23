using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Domain.Models;
using e_commerce.DTOs.Responses;

namespace e_commerce.Domain.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> all(int pageNumber, int pageSize);
        public Task<Product> getById(string id);
        public Task create(Product product);
        public Task update(Product product);
        public Task delete(string id);
        public Task<long> count();

        public Task<IEnumerable<ShoppingCartItem>> join(List<ProductItem> ids);
    }
}