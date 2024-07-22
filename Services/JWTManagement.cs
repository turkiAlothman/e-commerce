using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using e_commerce.Domain.Models;
using e_commerce.utils;
using Microsoft.IdentityModel.Tokens;

namespace e_commerce.Services
{
     public class JwtManagement
    {
        private readonly IConfiguration Configuration;
        public JwtManagement(IConfiguration Configuration){
            this.Configuration = Configuration;
        } 
        public String GenerateToken(string id = null,string Email = null,ProductItem[] products = null) {
                
            string key =  Configuration.GetValue<String>("PrivateKey")!;

            var hendler = new JwtSecurityTokenHandler();

            string Products_string ="[]";

            if(products != null)
                Products_string =  JsonSerializer.Serialize(products);
            
            var TokenDescriptor = new SecurityTokenDescriptor {
                
                Subject = new ClaimsIdentity(new[]{
                    new Claim("id", id),
                    new Claim(ClaimTypes.Country, "KSA"),
                    new Claim(ClaimTypes.Email, Email),
                    new Claim("products", Products_string),
                    new Claim("exp", new DateTimeOffset(DateTime.UtcNow.AddDays(1)).ToUnixTimeSeconds().ToString()),
                    new Claim("platform", "browser"),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),SecurityAlgorithms.HmacSha256Signature),
                };
            
            var token = hendler.CreateEncodedJwt(TokenDescriptor);

            return token;
        } 
    }
}