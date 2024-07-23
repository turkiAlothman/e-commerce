
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
        bool signedIn ;
        public CartController(IProductRepository productRepository, JwtService jwt , IHttpContextAccessor context){
            this._ProductRepository = productRepository;
            this.jwt = jwt;
            signedIn =  bool.Parse(context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "signed_in")!.Value);
        }


        [HttpPost("{ProductId}")]
        public async Task<IActionResult> AddItemToCart(string ProductId){
            
            Product product =  await _ProductRepository.getById(ProductId);
            
            if (product == null)
                return NotFound(new {message = "product not found"});
            
            // get signed_in claim
           
            
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
                    items.Add(new ProductItem{ProductId = product.Id ,Quentity = 1});
                
                string token = jwt.GenerateToken(products:items.ToArray());
                HttpContext.Response.Cookies.Append("jwt",token);
            }
            
            return Ok(new {message = ""});
            
        } 

        [HttpGet]
        public async Task<ActionResult> GetCart(){
            List<ProductItem> items =  JsonConvert.DeserializeObject<List<ProductItem>>(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "products")!.Value);
            return Ok(items);
        }
    
        [HttpDelete("{ProductId}")]
        public async Task<ActionResult> removeItem(string ProductId){
            
            Product product =  await _ProductRepository.getById(ProductId);
            
            if (product == null)
                return NotFound(new {message = "product not found"});

            string itemsString = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "products")!.Value;
            IList<ProductItem> items = JsonConvert.DeserializeObject<List<ProductItem>>(itemsString)!;
            for (int i = 0; i < items.Count(); i++)
            {
                if(items[i].ProductId == product.Id)
                    items[i].Quentity -= 1;
                
                if(items[i].Quentity == 0)
                    items.RemoveAt(i);
            }

            string token = jwt.GenerateToken(products:items.ToArray());
            HttpContext.Response.Cookies.Append("jwt",token);

            return Ok(new {message = "item removed successfully"});
        }

        


        [HttpDelete("remove/{ProductId}")]
        public async Task<ActionResult> removeProduct(string ProductId){
            
            Product product =  await _ProductRepository.getById(ProductId);
            
            if (product == null)
                return NotFound(new {message = "product not found"});

            string itemsString = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "products")!.Value;
            IList<ProductItem> items = JsonConvert.DeserializeObject<List<ProductItem>>(itemsString)!;

          for (int i = 0; i < items.Count(); i++)
          {
            if (items[i].ProductId == product.Id)
                items.RemoveAt(i);
          }

            return Ok(new {message = "Product removed successfully"});
        }

    
    }
}