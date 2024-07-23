
using e_commerce.Domain.Models;

namespace e_commerce.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> getById(string id);
        public Task<User> GetByEmail(string Email);
        public Task Create(User user);
    }
}