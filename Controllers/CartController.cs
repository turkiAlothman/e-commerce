using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
using e_commerce.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {

        private readonly IProductRepository _ProductRepository;
        public CartController(IProductRepository productRepository){
            this._ProductRepository = productRepository;
        }

        [HttpPost("{ProductId}")]
        public async Task<IActionResult> AddItemToCart(string ProductId){
            
            Product product;
            
            try
            {
                product =  await _ProductRepository.getById(ProductId);
            }
            catch (FormatException)
            {
                return NotFound(new {message = "product not found"});
            }
            
            if (product == null)
                return NotFound(new {message = "product not found"});
            
            HttpContext.User.Claims.FirstOrDefault(claim => {
                Debugging.print(claim.Issuer);
                Debugging.print(claim.Value);
                Debugging.print(claim.Subject);
                Console.WriteLine("\n\n");
                return claim.Value =="d";
            });
            return Ok(new {message = HttpContext.User.Claims.});
            
        } 
    }
}