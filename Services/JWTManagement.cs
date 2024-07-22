using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
            public String GenerateToken(string id = null,string Email = null,productPayload[] products = null) {
                string key =  Configuration.GetValue<String>("PrivateKey")!;

                var hendler = new JwtSecurityTokenHandler();

                string Products_string ="[]";

                if(products != null)
                    Products_string =  JsonSerializer.Serialize(products);
                
                 DateTime centuryBegin = new DateTime(1970, 1, 1);
                var TokenDescriptor = new SecurityTokenDescriptor {
                    
                    Subject = new ClaimsIdentity(new[]{
                        new Claim("id", id),
                        new Claim(ClaimTypes.Country, "KSA"),
                        new Claim(ClaimTypes.Email, Email),
                        new Claim("products", Products_string),
                        new Claim("platform", "browser"),
                        new Claim("exp", new DateTimeOffset(DateTime.UtcNow.AddDays(1)).ToUnixTimeSeconds().ToString()),

                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),SecurityAlgorithms.HmacSha256Signature),
                };
            
            var token = hendler.CreateEncodedJwt(TokenDescriptor);

            return token;
        } 




  public String GenerateTokennew(string id = null,string Email = null,productPayload[] products = null) {
                string key =  Configuration.GetValue<String>("PrivateKey")!;

               
                string Products_string ="[]";

                if(products != null)
                    Products_string =  JsonSerializer.Serialize(products);
                
                
                DateTime centuryBegin = new DateTime(1970, 1, 1);
                var exp = new TimeSpan(DateTime.Now.AddYears(1).Ticks - centuryBegin.Ticks).TotalSeconds;
                var now = new TimeSpan(DateTime.Now.Ticks - centuryBegin.Ticks).TotalSeconds;
                var payload = new System.IdentityModel.Tokens.Jwt.JwtPayload
                    {
                        {"platfprm", "true"},
                        {"browser", "true"},
                        {"iat", (long)now},
                        {"", ""},
                        {"name", "dd"},
                        {"id", "dd"},
                        {"exp", new DateTimeOffset(DateTime.UtcNow.AddDays(1)).ToUnixTimeSeconds() },
                        
                    };
                    var hendler = new JwtSecurityTokenHandler();
                    var header = new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256));
                    var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(header, payload);
                    
                                return hendler.WriteToken(token);
                            } 
 

    }

    public class productPayload{
        public string Id {get; set;}
        public int Quentity {get; set;}
    }

}