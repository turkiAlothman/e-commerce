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


            foreach (var item in ValidateToken(newToken).Claims)
            {
                Debugging.print(item);
            } 



            Debugging.print(jwtSecurityToken);
            
            await next.Invoke(context);
        }
    
    
    public static ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, 
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aEaAJ86htbtZ1a1TCTZIXHTFdbXU3jYuXBEA5Pya+5Y3q3gaY+HYx0UjhyeQxJVExAnhMvbNqLTVYFrYlwIDAQAB")),
            ClockSkew = TimeSpan.Zero // Optional: eliminate clock skew
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return principal;
        }
        catch (Exception ex)
        {
            // Token validation failed
            Debugging.print($"Token validation failed: {ex.Message}");
            return null;
        }
    }
    }
}