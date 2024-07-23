using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using e_commerce.Services;
using e_commerce.utils;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;

namespace e_commerce.Middlewares
{
    public class SessionManagementMiddleware
    {
        private readonly RequestDelegate next;
        private readonly JwtService jwt;

        public SessionManagementMiddleware(RequestDelegate next, JwtService jwt){
            this.next = next;
            this.jwt = jwt;
        }
        public async Task Invoke(HttpContext context){
            string token =  context.Request.Headers["Authorization"];

            if(token == null)
                token = jwt.GenerateToken();
            
            context.Request.Headers["Authorization"] = token;
            
            Debugging.print(new JwtSecurityTokenHandler().ReadToken(token));

            context.Response.Cookies.Append("jwt",token);
            
            await next.Invoke(context);

   
        }
    
    
    }
}