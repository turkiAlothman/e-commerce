
using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
using e_commerce.Services;
using e_commerce.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace e_commerce.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {

        private readonly IProductRepository _ProductRepository;
        private readonly JwtService jwt;
        public CartController(IProductRepository productRepository,JwtService jwt){
            this._ProductRepository = productRepository;
            this.jwt = jwt;
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
            
            // get signed_in claim
            bool signedIn =  bool.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "signed_in")!.Value);
            
            if(!signedIn){
                string productString = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "products")!.Value;
                List<ProductItem> items = JsonConvert.DeserializeObject<List<ProductItem>>(productString);
                
                Debugging.print(items.Count);

                bool found =false;
                foreach (ProductItem item in items)
                {
                    if(item.ProductId == product.Id)
                    {
                        if(item.Quentity + 1 > product.Quantity)
                            return UnprocessableEntity(new {message ="Insufficient product quantity available"});
                        found = true;
                        item.Quentity +=1;
                    }                        
                            
                }

                
                if(!found)
                    items.Add(new ProductItem{ProductId = product.Id ,Quentity =1});
                
                string token = jwt.GenerateToken(products:items.ToArray());
                HttpContext.Response.Cookies.Append("jwt","");
            }
            
            return Ok(new {message = ""});
            
        } 
    
        public async Task<ActionResult> GetCart(){
            List<Product> items =  JsonConvert.DeserializeObject<List<Product>>(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "products")!.Value);
            return Ok(items);
        }
    }
}