
using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
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
        private readonly IAuthorizationPolicyProvider _policyProvider;
        public CartController(IProductRepository productRepository,IAuthorizationPolicyProvider policyProvider){
            this._ProductRepository = productRepository;
            this._policyProvider = policyProvider;
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
            

            Debugging.print( (await _policyProvider.GetPolicyAsync("signed_in")).Requirements.First().ToString());
            if(!signedIn){
                string productString = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "products")!.Value;
                List<ProductItem> items = JsonConvert.DeserializeObject<List<ProductItem>>(productString);
                
                Debugging.print(items.Count);

                foreach (ProductItem item in items)
                {
                    if(item.Quentity + 1 > product.Quantity)
                        return UnprocessableEntity(new {message ="Insufficient product quantity available"});
                }

            }
            
            return Ok(new {message = ""});
            
        } 
    }
}