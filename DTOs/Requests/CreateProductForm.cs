using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.DTOs.Requests
{
    public class CreateProductForm
    {
        [Required]
        [StringLength(80)]
        public string productName {set;get;}
        
        [Required]
        [StringLength(200)]
        public string Describtion {set;get;}
        
        [Required]
        public double Price {set;get;} 
        
        [Required]
        public int Quantity{set;get;} = 0;
    }
}