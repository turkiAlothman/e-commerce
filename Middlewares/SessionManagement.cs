using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using e_commerce.Services;
using e_commerce.utils;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;

namespace e_commerce.Middlewares
{
    public class SessionManagement
    {
        private readonly RequestDelegate next;
        private readonly JwtManagement jwt;

        public SessionManagement(RequestDelegate next, JwtManagement jwt){
            this.next = next;
            this.jwt = jwt;
        }
        public async Task Invoke(HttpContext context){
            string newToken = jwt.GenerateToken("22","turkialothman@jfn"); 
            context.Request.Headers["Authorization"] = newToken;

            Debugging.print(newToken);
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(newToken);
            
            await next.Invoke(context);
        }
    
    
    }
}