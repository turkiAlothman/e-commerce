using e_commerce.Domain.Models;
using e_commerce.Domain.Repositories.Interfaces;
using e_commerce.DTOs.Requests;
using e_commerce.Extensions;
using e_commerce.utils;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Extensions;
using e_commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace e_commerce.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _CartRepository;
        private readonly JwtService jwt;

        public AuthController( IUserRepository userRepository, JwtService jwt, ICartRepository CartRepository )
        {
            this._userRepository = userRepository;
            this._CartRepository = CartRepository;
            this.jwt = jwt;
        }

        
        public IActionResult signin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> signin([FromForm] SignInForm form)
        {
            if(!ModelState.IsValid)
                return View(form);
            
            User user = await _userRepository.GetByEmail(form.Email);
            if(user == null || user.Password != form.Password.Hash())
            {
                ModelState.AddModelError("error", "username or password is not correct");
                return View(form);
            }
            
            string token = jwt.GenerateToken(user.Id,user.Email,Signed_up : true);
            HttpContext.Response.Cookies.Append("jwt",token);
            
               // check if there are items added to the cart so that we move it to the database;
            string productString = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "products")!.Value;
            List<ProductItem> items = JsonConvert.DeserializeObject<List<ProductItem>>(productString);
            
            Cart Cart =  await this._CartRepository.GetCart(user.Id);
            
            Cart.products.AddRange(items);
            await _CartRepository.update(Cart);
            

            return Redirect("/"); 

            
        }

        [HttpGet]
        public IActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> signup([FromForm] SignUpForm form)
        {
          if(! ModelState.IsValid)
            return View(form);

        User user = await _userRepository.GetByEmail(form.Email);
        if (user is not null)
        {
            ModelState.AddModelError("error", "email is already used");
            return View(form);
        }
            user = new User(form.FirstName, form.LastName, form.Email,form.Password.Hash());
            await _userRepository.Create(user);
            
            
            
            // check if there are items added to the cart so that we move it to the database;
            string productString = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "products")!.Value;
            List<ProductItem> items = JsonConvert.DeserializeObject<List<ProductItem>>(productString);
            
            Cart newCart = new Cart(user.Id);
            newCart.products.AddRange(items);
            await _CartRepository.create(newCart);

            
            string token = jwt.GenerateToken(user.Id,user.Email,Signed_up : true);
            HttpContext.Response.Cookies.Append("jwt",token);
            return Redirect("/");

            
        }

    }
}