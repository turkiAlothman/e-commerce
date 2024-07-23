
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
    [Route("api/Cart")]
    public class CartApiController : ControllerBase
    {

        private readonly IProductRepository _ProductRepository;
        private readonly ICartRepository _CartRepository;
        private readonly JwtService jwt;
        bool signedIn ;
        string UserId ;
        public CartApiController(IProductRepository productRepository,ICartRepository CartRepository, JwtService jwt , IHttpContextAccessor context){
            
            this._ProductRepository = productRepository;
            this._CartRepository = CartRepository;
            this.jwt = jwt;
            signedIn =  bool.Parse(context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "signed_in")!.Value);
            UserId = context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "id")!.Value;
        }


        
        [HttpPost("{ProductId}")]
        public async Task<IActionResult> AddItemToCart(string ProductId){
            
            Product product =  await _ProductRepository.getById(ProductId);

            
            if (product == null)
                return NotFound(new {message = "product not found"});
                       
            List<ProductItem> items;
            Cart cart = null;
            
            // get the cart from database if authenticated(signed_in) otherwise get from session

            if(!signedIn)
            {
                string productString = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "products")!.Value;
                items = JsonConvert.DeserializeObject<List<ProductItem>>(productString);
            }
            else
            {
                cart = await this._CartRepository.GetCart(UserId);
                items = cart.products;
            }


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
                
                
                // update cart in database if authenticated(signed_in) otherwise update in session
                if (!signedIn)
                {
                    string token = jwt.GenerateToken(products:items.ToArray());
                    HttpContext.Response.Cookies.Append("jwt",token);                    
                }
                else
                {
                    await this._CartRepository.update(cart);
                }

            
            
            return Ok(new {message = "updates successfully"});
            
        } 

        
        
        
        
        [HttpGet]
        public async Task<ActionResult> GetCart(){
            List<ProductItem> items;
            Cart cart = null;
            
            if(!signedIn)
                items =  JsonConvert.DeserializeObject<List<ProductItem>>(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "products")!.Value);
            else{
                cart = await this._CartRepository.GetCart(UserId);
                items = cart.products;
            }
            
            return Ok(items);
        }
    
        
        
        
        
        [HttpDelete("{ProductId}")]
        public async Task<ActionResult> removeItem(string ProductId){
            
            Product product =  await _ProductRepository.getById(ProductId);
            
            
            if (product == null)
                return NotFound(new {message = "product not found"});

            List<ProductItem> items;
            Cart cart = null;
            
            if(!signedIn)
            {
                string itemsString = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "products")!.Value;
                items = JsonConvert.DeserializeObject<List<ProductItem>>(itemsString)!;
            }
            else
            {
                cart = await this._CartRepository.GetCart(UserId);
                items = cart.products;
            }
            
            
            for (int i = 0; i < items.Count(); i++)
            {
                if(items[i].ProductId == product.Id)
                    items[i].Quentity -= 1;
                
                if(items[i].Quentity == 0)
                    items.RemoveAt(i);
            }

            if (!signedIn)
            {
                string token = jwt.GenerateToken(products:items.ToArray());
                HttpContext.Response.Cookies.Append("jwt",token);    
            }
            else
            {
                await this._CartRepository.update(cart);
            }
            

            return Ok(new {message = "item removed successfully"});
        }

        


        [HttpDelete("remove/{ProductId}")]
        public async Task<ActionResult> removeProduct(string ProductId){
            
            Product product =  await _ProductRepository.getById(ProductId);
            
            if (product == null)
                return NotFound(new {message = "product not found"});

            List<ProductItem> items;
            Cart cart = null;
            
            if(!signedIn)
            {
                string itemsString = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "products")!.Value;
                items = JsonConvert.DeserializeObject<List<ProductItem>>(itemsString)!;
            }
            else
            {
                cart = await this._CartRepository.GetCart(UserId);
                items = cart.products;
            }

          for (int i = 0; i < items.Count(); i++)
          {
            if (items[i].ProductId == product.Id)
                items.RemoveAt(i);
          }

            if(!signedIn)
            {
                string token = jwt.GenerateToken(products:items.ToArray());
                HttpContext.Response.Cookies.Append("jwt",token);    
            }
            else
            {
                await this._CartRepository.update(cart);
            }

            return Ok(new {message = "Product removed successfully"});
        }

    
    }
}