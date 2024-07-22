using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Domain.Models;

namespace e_commerce.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> getById(string id);
        public Task<User>  GetByEmail(Product Email); 
    }
}