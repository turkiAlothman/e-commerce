using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.errors
{
    public interface BaseError
    {
        public int StatusCode { get; set; }
        public string meesage { get; set; }
    }
}