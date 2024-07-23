using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Domain.Models;

namespace e_commerce.DTOs.Responses
{
    public class ShoppingCartItem
    {
        public Product product{ get; set;}
        public int count { get; set;}
    }
}