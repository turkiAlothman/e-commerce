using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Domain.Models
{
    public class Cart : BaseModel
    {
        public string UserId { get; set; }
        public IList<ProductItem> products { get; set; } = [];
    }
}