using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Domain.Models
{
    public class Cart : BaseModel
    {
        public Cart(string UserId){
            this.UserId = UserId;
        }
        public string UserId { get; set; }
        public List<ProductItem> products { get; set; } = [];
    }
}