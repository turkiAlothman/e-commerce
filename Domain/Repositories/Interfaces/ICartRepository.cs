using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Domain.Models;
using e_commerce.DTOs.Responses;

namespace e_commerce.Domain.Repositories.Interfaces
{
    public interface ICartRepository
    {
        public  Task<Cart> GetCart(string UserId);
        public  Task update(Cart cart);
        
    }
}