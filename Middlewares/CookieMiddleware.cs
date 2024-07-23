using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using e_commerce.Services;
using e_commerce.utils;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;

namespace e_commerce.Middlewares
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate next;
        private readonly JwtService jwt;

        public CookieMiddleware(RequestDelegate next, JwtService jwt){
            this.next = next;
            this.jwt = jwt;
        }
        public async Task Invoke(HttpContext context){
            context.Request.Headers["Authorization"]  =  context.Request.Cookies["jwt"];
            
            await next.Invoke(context);

   
        }
    
    
    }
}