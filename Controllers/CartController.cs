
using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace e_commerce.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CartController : Controller
    {
         private readonly IProductRepository _ProductRepository;
        private readonly ICartRepository _CartRepository;
        bool signedIn ;
        string UserId ;
        public CartController(IProductRepository productRepository,ICartRepository CartRepository, IHttpContextAccessor context){
            
            this._ProductRepository = productRepository;
            this._CartRepository = CartRepository;
            signedIn =  bool.Parse(context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "signed_in")!.Value);
            UserId = context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "id")!.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ProductItem> items;
            Cart cart;

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


            return View(await this._ProductRepository.join(items));
        }

    }
}